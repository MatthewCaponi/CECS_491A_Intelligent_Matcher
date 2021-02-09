using System;

namespace UserAccessControl
{ 
    public interface IScope {

        void GetComponentsAccessible(List<string> componentsAccessible);

        void GetActionsAccessible(List<string> actionsAccessible);

        void SetComponentsAccessible(List<string> componentsAccessible);

        void SetActionsAccessible(List<string> actionsAccessible);
    }
}
