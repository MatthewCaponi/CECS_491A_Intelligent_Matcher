using System;
using System.Collections.Generic;

namespace UserAccessControl
{
    public interface IScope
    {
        void GetComponentsAccessible(List<string> componentsAccessible);

        void GetActionsAccessible(List<string> actionAccessible);

        void SetComponentsAccessible(List<string> componentsAccessible);

        void SetActionsAccessible(List<string> actionAccessible);
    }
}
