using System;
using System.Collections.Generic;

namespace Piratenverein.Models;

public partial class Pirat
{
    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string Spitzname { get; set; } = null!;

    public int Jahresalter { get; set; }
}
