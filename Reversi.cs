//Reversi door Charlotte en Bastiaan

using System;
using System.Drawing;
using System.Windows.Forms;

public class Scherm : Form
{
    Knop nieuwSpel = new Knop("Nieuw spel", new Point(20,20));
    Knop hint = new Knop("Hint", new Point(20, 64));

    public String[] SpelerNamen = { "Speler 1", "Speler 2" };
    public bool HintsToegestaan = true;

    Tekst speler1Naam;
    Tekst speler2Naam;
    Tekst speler1Score = new Tekst("0", new Point(50, 140));
    Tekst speler2Score = new Tekst ("0",new Point(50, 200));

    Tekst beurten = new Tekst ("Beurt: ", new Point(50, 230));

    public int VeldBreedte = 6;
    public int VeldHoogte = 6;

    Veld speelVeld;

    OptieScherm optieScherm;

    public Scherm() 
    {
        //Scherm dat de namen van de spelers vraag wordt aangeroepen
        this.optieScherm = new OptieScherm(this);
        this.optieScherm.ShowDialog(); 

        this.speelVeld = new Veld(new Point(180, 20), VeldHoogte, VeldBreedte, this.SpelerNamen);
        this.StartPosition = FormStartPosition.CenterScreen;

        this.schermGrootte();

        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        this.Paint += tekenStenen;
        this.DoubleBuffered = true;

        this.nieuwSpel.Click += klik;
        this.Controls.Add(this.nieuwSpel);

        this.hint.Click += klik;
        this.Controls.Add(this.hint);

        speler1Naam = new Tekst(SpelerNamen[0], new Point(50, 110));
        speler2Naam = new Tekst(SpelerNamen[1], new Point(50, 170));
        this.Controls.Add(this.speler1Naam);
        this.Controls.Add(this.speler2Naam);
        this.Controls.Add(this.speler1Score);
        this.Controls.Add(this.speler2Score);

        this.Controls.Add(this.beurten);

        this.speelVeld.Paint += this.updateScherm;
        this.Controls.Add(this.speelVeld);
    }

    private void schermGrootte()
    {
        //Reset maximum grootte, zodat de grootte daarbuiten kan worden aangepast
        this.MaximumSize = new Size();

        //Grootte van het scherm wordt bepaald door het aantal vakjes in het speelveld
        this.ClientSize = new Size(200 + this.speelVeld.vakGrootte * this.speelVeld.veldBreedte, 40 + this.speelVeld.vakGrootte * this.speelVeld.veldHoogte);
        if (this.ClientSize.Height < 210) this.ClientSize = new Size(200 + this.speelVeld.vakGrootte * this.speelVeld.veldBreedte, 280);
        
        //Stel maximum grootte weer in op de huidige grootte, zodat er niet geresized kan worden
        this.MaximumSize = this.Size;

        //Plaats scherm weer in het midden
        this.CenterToScreen();
    }

    //Methode om de stenen te tekenen die de kleuren van beide spelers in de UI tonen
    private void tekenStenen(object obj, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //Speler pictogrammen
        gr.FillEllipse(this.speelVeld.Speler1Brush, 20, 114, 20, 20);
        gr.FillEllipse(this.speelVeld.Speler2Brush, 20, 174, 20, 20);
        
        //Beurt pictogram
        if (this.speelVeld.Beurt % 2 == 0) gr.FillEllipse(this.speelVeld.Speler1Brush, 20, 234, 20, 20);
        else if (this.speelVeld.Beurt % 2 == 1) gr.FillEllipse(this.speelVeld.Speler2Brush, 20, 234, 20, 20);

        //Cirkels om de pictogrammen voor betere zichtbaarheid
        gr.DrawEllipse(Pens.Black, 20, 114, 20, 20);
        gr.DrawEllipse(Pens.Black, 20, 174, 20, 20);
        gr.DrawEllipse(Pens.Black, 20, 234, 20, 20);
    }

    private void klik(object sender, EventArgs e)
    {
        //Start een nieuw spel klik
        if (sender == this.nieuwSpel)
        {
            this.optieScherm.ShowDialog();

            this.Controls.Remove(this.speelVeld);

            this.speelVeld = new Veld(new Point(180, 20), VeldHoogte, VeldBreedte, this.SpelerNamen);
            this.speelVeld.Paint += this.updateScherm;
            this.Controls.Add(speelVeld);

            this.schermGrootte();
        }
        //Geeft hints weer klik
        else if (sender == this.hint)
        {
            this.speelVeld.Hint = true;
            this.speelVeld.Invalidate();
        }
    }

    //Functie voor het dynamisch updaten van het scherm aan de hand van speelVeld events
    public void updateScherm(object obj, PaintEventArgs pea)
    {
        this.speler1Naam.Text = SpelerNamen[0];
        this.speler2Naam.Text = SpelerNamen[1];

        this.speler1Score.Text = "Stenen: " + Convert.ToString(this.speelVeld.Score(1));
        this.speler2Score.Text = "Stenen: " + Convert.ToString(this.speelVeld.Score(2));

        this.beurten.Text = "Beurten: " + Convert.ToString(this.speelVeld.Beurt + 1);

        if (this.speelVeld.Status != "") this.Text = "Reversi - " + this.speelVeld.Status;
        else this.Text = "Reversi";

        //Knop voor hints kan niet worden gebruikt als hints niet zijn toegestaan
        if (this.speelVeld.Gestart && this.HintsToegestaan) this.hint.Enabled = true;
        else this.hint.Enabled = false;

        //Opnieuw tekenen zodat o.a. het beurt pictogram wordt aangepast
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