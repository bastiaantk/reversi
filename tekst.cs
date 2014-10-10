using System;
using System.Windows.Forms;
using System.Drawing;

//Klasse met waarden voor invoerlabels
public class Tekst : Label
{
    public Tekst(String tekst, Point locatie)
    {
        this.Text = tekst;
        this.Location = locatie;
        this.Font = new Font("Corbel", 14);
        this.Size = new Size(140, 30);
        this.Padding = new Padding(0, 3, 0, 0);
    }
}