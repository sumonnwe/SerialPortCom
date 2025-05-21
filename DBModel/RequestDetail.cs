using System;
using System.Collections.Generic;

namespace NETSFUNCTION.DBModel
{
public partial class RequestDetail
{
    public string Ecn { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldValue { get; set; }
}

}