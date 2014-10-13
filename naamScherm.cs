using System;
using System.Drawing;
using System.Windows.Forms;

//Scherm voor het invoeren van spelernamen
public class NaamScherm : Form
{
    public Invoer naam1 = new Invoer("", new Point(40, 55));
    public Invoer naam2 = new Invoer("", new Point(40, 105));
    Scherm spelScherm;

    public NaamScherm(Scherm spelScherm)
    {
        this.spelScherm = spelScherm;

        this.Text = "Reversi - Namen";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ClientSize = new Size(220, 240);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

        Tekst naamLabel = new Tekst("Namen:", new Point(40, 20));
        this.Controls.Add(naamLabel);

        this.naam1.Text = spelScherm.spelerNamen[0];
        this.Controls.Add(this.naam1);

        this.naam2.Text = spelScherm.spelerNamen[1];
        this.Controls.Add(this.naam2);

        Knop naamKnop = new Knop("Spelen", new Point(40, 185));
        naamKnop.Click += this.opslaan;
        this.Controls.Add(naamKnop);
    }

    private void opslaan(object obj, EventArgs e)
    {
        this.spelScherm.spelerNamen[0] = this.naam1.Text;
        this.spelScherm.spelerNamen[1] = this.naam2.Text;
        this.Close();
    }
}