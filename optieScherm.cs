using System;
using System.Drawing;
using System.Windows.Forms;

//Scherm voor het invoeren van spelernamen
public class OptieScherm : Form
{
    public Invoer naam1 = new Invoer("", new Point(40, 55));
    public Invoer naam2 = new Invoer("", new Point(40, 105));
    public CheckBox hints = new CheckBox();
    Scherm spelScherm;

    public OptieScherm(Scherm spelScherm)
    {
        this.spelScherm = spelScherm;

        this.Text = "Reversi - Opties";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ClientSize = new Size(220, 255);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

        this.Controls.Add(new Tekst("Spelers:", new Point(40, 20)));

        this.naam1.Text = spelScherm.SpelerNamen[0];
        this.naam1.MaxLength = 10;
        this.Controls.Add(this.naam1);

        this.naam2.Text = spelScherm.SpelerNamen[1];
        this.naam2.MaxLength = 10;
        this.Controls.Add(this.naam2);

        this.hints.Text = "Hints";
        this.hints.Font = new Font("Corbel", 14);
        this.hints.Checked = true;
        this.hints.Location = new Point(40, 155);
        this.Controls.Add(this.hints);

        Knop naamKnop = new Knop("Spelen", new Point(40, 190));
        naamKnop.Click += this.opslaan;
        this.Controls.Add(naamKnop);
    }

    private void opslaan(object obj, EventArgs e)
    {
        //Opties worden voor het spelScherm ingesteld
        this.spelScherm.HintsToegestaan = this.hints.Checked;
        this.spelScherm.SpelerNamen[0] = this.naam1.Text;
        this.spelScherm.SpelerNamen[1] = this.naam2.Text;
        this.Close();
    }
}