using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Kampf
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public KampfStatus Status { get; set; }

        public List<KampfTeilnehmer> Teilnehmer { get; set; } = new();
        public List<KampfLog> Logs { get; set; } = new();
    }
}