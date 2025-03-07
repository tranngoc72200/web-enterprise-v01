using System;
using System.Security.Claims;
using System.Security.AccessControl;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebEnterprise_mssql.Api.Configuration;
using WebEnterprise_mssql.Api.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebEnterprise_mssql.Api.Data;
using WebEnterprise_mssql.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebEnterprise_mssql.Api.Controllers
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtConfig jwtConfig;
        private readonly TokenValidationParameters tokenValidationParams;
        private readonly ApiDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthManagementController> logger;

        public AuthManagementController(
            ILogger<AuthManagementController> logger,
            UserManager<ApplicationUser> userManager, 
            IOptionsMonitor<JwtConfig> optionsManager,
            TokenValidationParameters tokenValidationParams,
            RoleManager<IdentityRole> roleManager,
            ApiDbContext context)
        {
            this.logger = logger;
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
            this.jwtConfig = optionsManager.CurrentValue;
            this.tokenValidationParams = tokenValidationParams;
        }

        [HttpGet("{token}")]
        [Route("GetUser")]
        public async Task<UserProfileResponseDto> GetUserProfileAsync(string token) {
            var user = await DecodeToken(token);
            var userProfileDto = new UserProfileResponseDto() {
                username = user.UserName,
                email = user.Email
            };
            return userProfileDto;
        }
        
        [HttpPost] 
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UsersRegistrationDto usersRegistrationDto) {
            
            if(ModelState.IsValid) {
                
                var existingEmail = await userManager.FindByEmailAsync(usersRegistrationDto.Email);
                
                if(existingEmail is not null) {
                    return BadRequest(new RegistrationResponseDto() {
                       Errors = new List<string>()  {
                           "User Email already exist!!!"
                       },
                       Success = false
                    });
                }

                var newUser = new ApplicationUser() {
                    Email = usersRegistrationDto.Email,
                    UserName = usersRegistrationDto.Username
                };

                var isCreated = await userManager.CreateAsync(newUser, usersRegistrationDto.Password);
                if(isCreated.Succeeded) {
                    
                    //Add the user to a role
                    //await userManager.AddToRoleAsync(newUser, "staff");

                    var jwttoken = await GenerateJwtToken(newUser);

                    return Ok(new {
                        jwttoken
                        //message = "the account was assigned with role Staff by default"
                    });

                } else {
                    return BadRequest(new RegistrationResponseDto() {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }
            return BadRequest(new RegistrationResponseDto() {
                Errors = new List<string>() {
                    "Invalid Payload!!!"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLogin) {
            
            if(ModelState.IsValid) {
                var existingUser = await userManager.FindByEmailAsync(userLogin.Email);

                if(existingUser is null) {
                    return BadRequest(new RegistrationResponseDto() {
                        Errors = new List<string>() {
                            "Email is not exist!!"
                        }
                    });
                }

                var isCorrect = await userManager.CheckPasswordAsync(existingUser, userLogin.Password);

                if(!isCorrect) {
                    return BadRequest(new RegistrationResponseDto() {
                        Errors = new List<string>() {
                            "Password is not correct!!"
                        }
                    });
                }

                var jwtToken = await GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }
            return BadRequest(new RegistrationResponseDto() {
                Errors = new List<string>() {
                    "Ivalid Paylaod"
                }
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> Refreshtoken([FromBody] TokenRequestDto tokenRequestDto) {
            if(ModelState.IsValid) {
                var result = await VerifyAndGenerateToken(tokenRequestDto);

                if(result is null) {
                    return BadRequest(new RegistrationResponseDto() {
                        Errors = new List<string>() {
                            "Invalid Token!!!",
                        },
                        Success = false
                    });
                }

                return Ok(result);
            }
            return BadRequest(new RegistrationResponseDto() {
                Errors = new List<string>() {
                    "Invalid Payload!!!",
                },
                Success = false
            });
        }

        private async Task<ApplicationUser> DecodeToken(string Authorization)
        {

            string[] Collection = Authorization.Split(" ");

            //Decode the token
            var stream = Collection[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            //get the user
            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            var user = await userManager.FindByEmailAsync(email);

            //return the user
            return user;
        }

        private async Task<AuthResult> GenerateJwtToken(ApplicationUser user) {
            var JwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var claims = await GetAllValidclaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(jwtConfig.ExpiryTimeFrame),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = JwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken() {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();

            return new AuthResult() {
                Token = jwtToken,
                Success = true,
                //RefreshToken = refreshToken.Token
            };
        }


        //Get all vailda Claims for the user
        private async Task<List<Claim>> GetAllValidclaims(ApplicationUser user) {
            var options = new IdentityOptions();

            var claims = new List<Claim> {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Getting the claims that we have assigned to the user
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Get the user role and add to the user
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await roleManager.FindByNameAsync(userRole);

                if(role is not null) {
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }


        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequestDto tokenRequestDto) {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequestDto.Token, tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if(result == false) {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if(expiryDate > DateTime.UtcNow) {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has not yet expired"
                        }
                    };
                }

                // validation 4 - validate existence of the token
                var storedToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestDto.RefreshToken);

                if(storedToken == null)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token does not exist"
                        }
                    };
                }

                // Validation 5 - validate if used
                if(storedToken.IsUsed)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been used"
                        }
                    };
                }

                // Validation 6 - validate if revoked
                if(storedToken.IsRevoked)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been revoked"
                        }
                    };
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if(storedToken.JwtId != jti)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token doesn't match"
                        }
                    };
                }

                // Validation 8 - validate stored token expiry date
                if (storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>() {
                            "Refresh token has expired"
                        }
                    };
                }

                //update current token

                storedToken.IsUsed = true;
                context.RefreshTokens.Update(storedToken);
                await context.SaveChangesAsync();


                var dbUser = await userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Lifetime validation failed. The token is expired.")) {

                      return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has expired please re-login"
                        }
                    };
                
                } else {
                      return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Something went wrong."
                        }
                    };
                }
            }
        }

        private DateTime UnixTimeStampToDateTime(long UnixTimeStamp)
        {
            var datetimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            datetimeVal = datetimeVal.AddSeconds(UnixTimeStamp).ToLocalTime();
            return datetimeVal;
        }

        private string RandomString(int length) {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYIZ0123456789";
            
            return new string(Enumerable.Repeat(chars, length)
                                .Select(x => x[random.Next(x.Length)]).ToArray());
        }
    }
}