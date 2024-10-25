using System;
using System.Collections.Generic;

namespace UESAN.StoreDB.DOMAIN.Core.Entities;

public partial class User
{
    public long Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Country { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public string? Type { get; set; }

    public long? IdPersonal { get; set; }

    public virtual Personal? IdPersonalNavigation { get; set; }
}
