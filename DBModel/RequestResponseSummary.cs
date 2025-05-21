using System;
using System.Collections.Generic;

namespace NETSFUNCTION.DBModel
{
public partial class RequestResponseSummary
{
    public int Id { get; set; }

    public string Ecn { get; set; } = null!;

    public string FunctionCode { get; set; } = null!;

    public DateTime RequestDateTime { get; set; }

    public string? ResponseCode { get; set; }

    public DateTime? ResponeDateTime { get; set; }

    public byte Completed { get; set; }
}

}