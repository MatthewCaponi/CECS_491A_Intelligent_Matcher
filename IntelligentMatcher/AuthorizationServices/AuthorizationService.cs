using BusinessModels.UserAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserAccessControlServices
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool ValidateAccessPolicy(ClaimsPrincipal claimsPrincipal, AccessPolicyModel accessPolicy)
        {
            var claims = claimsPrincipal.Claims;
            var scopes = claimsPrincipal.Scopes;

            foreach (var scope in accessPolicy.Scopes)
            {
                string [] tempSubScope = scope.Split(':');
                string policyOperation = tempSubScope[1];
                string[] tempSubScopes = tempSubScope[0].Split('.');
                List<string> policySubScopes = tempSubScopes.ToList();

                var userScopes = scopes.Where(a => a.Contains(policySubScopes[0]));

                if (userScopes.Count() == 0)
                {
                    return false;
                }

                var validScope = false;
                foreach (var userScope in userScopes)
                {
                    string[] tempUserSubScope = userScope.Split(':');
                    string userOperation = tempUserSubScope[1];
                    string[] tempUserSubScopes = tempUserSubScope[0].Split('.');
                    List<string> userSubScopes = tempSubScopes.ToList();

                    if (policyOperation == "read")
                    {
                        if (userOperation != "read" && userOperation != "write")
                        {
                            Console.WriteLine("User Operation: " + userOperation);
                            continue;
                        }
                    }
                    else if (policyOperation == "write")
                    {
                        if (userOperation != "write")
                        {
                            continue;
                        }
                    }
                    else if (policyOperation == "delete")
                    {
                        if (userOperation != "delete")
                        {
                            continue;
                        }
                    }

                    Console.WriteLine(userSubScopes.Count);
                    Console.WriteLine(policySubScopes.Count);
                    if (userSubScopes.Count > policySubScopes.Count)
                    {
                        continue;
                    }

                    if (userSubScopes.Count <= policySubScopes.Count)
                    {
                        var success = true;
                        foreach (var userSubScope in userSubScopes)
                        {
                            if (!policySubScopes.Contains(userSubScope))
                            {
                                success = false;
                                break;
                            }
                        }

                        if (!success)
                        {
                            continue;
                        }
                        else
                        {
                            validScope = true;
                            break;
                        }
                    }
                }

                if (!validScope)
                {
                    return false;
                }

                return true;
            }

            foreach (var claim in accessPolicy.Claims)
            {
                var key = claims.Where(a => a.Type == claim.Type).FirstOrDefault();

                if (key == null)
                {
                    return false;
                }

                if (key.Value.Contains(claim.Value))
                {
                    Console.WriteLine("comparing" + key.Value + " with " + claim.Value + " on the type of " + key.Type);

                    return false;
                }
            }

            return true;
        }
    }
}
