using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.CrossCuttingConcerns
{
    public enum ErrorMessage
    {
        None,
        AsyncError,
        Null,
        UsernameExists,
        EmailExists,
        UserDoesNotExist,
        UserIsNotActive,
        UserIsActive,
        NoUsersExist,
        NoMatch,
        TooManyAttempts,
        CodeExpired,
        InvalidPassword
    }
}
