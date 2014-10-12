using System;
using System.Windows.Forms;
using System.Drawing;

//Klasse met waarden voor invoervelden
public class Invoer : TextBox
{
    public Invoer(String tekst, Point locatie)
    {
        this.Text = tekst;
        this.Location = locatie;
        this.Font = new Font("Corbel", 14);
        this.Size = new Size(140, 30);
    }
}