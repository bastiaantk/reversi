//Reversi door Charlotte en Bastiaan

using System;
using System.Drawing;
using System.Windows.Forms;

public class Scherm : Form
{
    Knop nieuwSpel = new Knop("Nieuw spel", new Point(20,20));
    Knop help = new Knop("Help", new Point(20, 74));
    Tekst speler1Stenen = new Tekst("aantal stenen", new Point(20,90));
    Tekst speler2Stenen = new Tekst ("aantal stenen",new Point(20, 120));
    Tekst beurten = new Tekst ("aan zet", new Point(20,150));
    const int veldBreedte = 10;
    const int veldHoogte = 10;
    Veld speelVeld = new Veld(new Point(180, 20), veldHoogte, veldBreedte);

    public Scherm() 
    {
        this.ClientSize = new Size(200 + speelVeld.vakGrootte * veldBreedte, 40 + speelVeld.vakGrootte * veldHoogte);

        this.Text = "Reversi";

        this.nieuwSpel.Click += klik;
        this.Controls.Add(this.nieuwSpel);

        this.Controls.Add(this.help);
        this.Controls.Add(this.speler1Stenen);
        this.Controls.Add(this.speler2Stenen);
        this.Controls.Add(this.beurten);

        this.speelVeld.Paint += this.updateScherm;
        this.Controls.Add(this.speelVeld);

        this.beurten.Text = "Beurt: " + Convert.ToString(this.speelVeld.beurt);
    }

    private void klik(object sender, EventArgs e)
    {
        if (sender == this.nieuwSpel)
        {
            this.speelVeld.initVeld();
            this.speelVeld.Invalidate();
        }
    }

    public void updateScherm(object obj, PaintEventArgs pea)
    {
        this.beurten.Text = "Beurt: " + Convert.ToString(this.speelVeld.beurt);
    }
}

public class Reversi
{
    public static void Main() 
    {
        Application.Run(new Scherm());
    }
}