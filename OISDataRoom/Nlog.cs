﻿using System;
using System.Collections.Generic;

namespace OISPublic.OISDataRoom;

public partial class Nlog
{
    public int Id { get; set; }

    public string? MachineName { get; set; }

    public DateTime? Logged { get; set; }

    public string? Level { get; set; }

    public string? Message { get; set; }

    public string? Logger { get; set; }

    public string? Properties { get; set; }

    public string? Callsite { get; set; }

    public string? Exception { get; set; }
}
