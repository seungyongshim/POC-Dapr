namespace BlazorApp.Shared;

public record UserActorState
(
    Company Company,
    Department Department,
    BackOfficeUser BackOfficeUser,
    string Reason,
    DateTime DueDate
);

public record Company
(
    string Code,
    string Name
);

public record Department
(
    string Code,
    string Name
);

public record BackOfficeUser
(
    string Name,
    Email Email
);

public record Email
(
    string Value
);
