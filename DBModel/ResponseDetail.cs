using System;
using System.Collections.Generic;

namespace NETSFUNCTION.DBModel
{
public partial class ResponseDetail
{
    public string Ecn { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldValue { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
}
