using System;
using System.Windows.Forms;
using System.Drawing;

public class Knop : Button
{
    public Knop(String tekst, Point locatie, Size size = default(Size))
    {
        this.Text = tekst;
        this.Location = locatie;
        if (size == default(Size)) this.Size = new Size(140, 34);
        else this.Size = size;
        this.Font = new Font("Corbel", 14);
    }
}