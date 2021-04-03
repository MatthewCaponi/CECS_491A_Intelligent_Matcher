using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
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
        EmailNotSent,
        NoMatch,
        TooManyAttempts,
        CodeExpired
    }
}
