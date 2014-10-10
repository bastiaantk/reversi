//Reversi door Charlotte en Bastiaan

using System;
using System.Drawing;
using System.Windows.Forms;

public class Scherm : Form
{
    Knop nieuwSpel = new Knop("Nieuw spel", new Point(20,20));
    Knop hint = new Knop("Hint", new Point(20, 64));
    Tekst speler1Score = new Tekst("Speler 1: 0", new Point(45, 110));
    Tekst speler2Score = new Tekst ("Speler 2: 0",new Point(45, 140));
    Tekst beurten = new Tekst ("Beurt: ", new Point(45, 170));
    const int veldBreedte = 10;
    const int veldHoogte = 10;
    Veld speelVeld = new Veld(new Point(180, 20), veldHoogte, veldBreedte);

    public Scherm() 
    {
        this.ClientSize = new Size(200 + speelVeld.vakGrootte * veldBreedte, 40 + speelVeld.vakGrootte * speelVeld.veldHoogte);
        if (this.ClientSize.Height < 210) this.ClientSize = new Size(200 + speelVeld.vakGrootte * speelVeld.veldBreedte, 210);

        this.Text = "Reversi";
        this.Paint += tekenStenen;

        this.nieuwSpel.Click += klik;
        this.Controls.Add(this.nieuwSpel);

        this.hint.Click += klik;
        this.Controls.Add(this.hint);

        this.Controls.Add(this.speler1Score);
        this.Controls.Add(this.speler2Score);
        this.Controls.Add(this.beurten);

        this.speelVeld.Paint += this.updateScherm;
        this.Controls.Add(this.speelVeld);

        this.beurten.Text = "Beurt: " + Convert.ToString(this.speelVeld.beurt);
    }

    private void tekenStenen(object obj, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        gr.FillEllipse(this.speelVeld.speler1Brush, 20, 114, 20, 20);
        gr.FillEllipse(this.speelVeld.speler2Brush, 20, 144, 20, 20);
        if (this.speelVeld.beurt == 0) gr.FillEllipse(Brushes.Gray, 20, 174, 20, 20);
        else if (this.speelVeld.beurt % 2 == 0) gr.FillEllipse(this.speelVeld.speler1Brush, 20, 174, 20, 20);
        else if (this.speelVeld.beurt % 2 == 1) gr.FillEllipse(this.speelVeld.speler2Brush, 20, 174, 20, 20);
    }

    private void klik(object sender, EventArgs e)
    {
        if (sender == this.nieuwSpel)
        {
            this.speelVeld.initVeld();
            this.speelVeld.startSpel();
            this.speelVeld.Invalidate();
        }
        else if (sender == this.hint)
        {
            this.speelVeld.hint = true;
            this.speelVeld.Invalidate();
        }
    }

    public void updateScherm(object obj, PaintEventArgs pea)
    {
        this.speler1Score.Text = "Speler 1: " + Convert.ToString(this.speelVeld.score(1));
        this.speler2Score.Text = "Speler 2: " + Convert.ToString(this.speelVeld.score(2));
        this.beurten.Text = "Beurt: " + Convert.ToString(this.speelVeld.beurt);
        this.Invalidate();
    }
}

public class Reversi
{
    public static void Main() 
    {
        Application.Run(new Scherm());
    }
}