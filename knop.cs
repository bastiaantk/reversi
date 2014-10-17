using System;
using System.Windows.Forms;
using System.Drawing;

public class Knop : Button
{
    public Knop(String tekst, Point locatie, Size grootte = default(Size))
    {
        this.Text = tekst;
        this.Location = locatie;

        //Als er geen grootte is ingesteld, wordt deze standaard ingesteld
        if (grootte == default(Size)) this.Size = new Size(140, 34);
        else this.Size = grootte;

        this.Font = new Font("Corbel", 14);
    }
}