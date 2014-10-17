using System;
using System.Drawing;
using System.Windows.Forms;

//Scherm voor het invoeren van spelopties
public class OptieScherm : Form
{
    public Invoer Naam1 = new Invoer("", new Point(40, 55));
    public Invoer Naam2 = new Invoer("", new Point(40, 105));
    
    public Invoer Hoogte = new Invoer("6", new Point(200, 125));
    public Invoer Breedte = new Invoer("6", new Point(200, 55));

    public CheckBox Hints = new CheckBox();

    Knop optieKnop = new Knop("Spelen", new Point(40, 190));
    Knop resetKnop = new Knop("Reset", new Point(200, 190));

    Scherm spelScherm;

    public OptieScherm(Scherm spelScherm)
    {
        this.spelScherm = spelScherm;
        Naam1.TabIndex = 0;
        Naam2.TabIndex = 1;
        Hoogte.TabIndex = 2;
        Breedte.TabIndex = 3;
        Hints.TabIndex = 4;

        this.Text = "Reversi - Opties";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.ClientSize = new Size(330, 255);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

        this.Controls.Add(new Tekst("Spelers:", new Point(40, 20)));

        //Naam speler 1
        this.Naam1.Text = spelScherm.SpelerNamen[0];
        this.Naam1.MaxLength = 10;
        this.Controls.Add(this.Naam1);

        //Naam speler 2
        this.Naam2.Text = spelScherm.SpelerNamen[1];
        this.Naam2.MaxLength = 10;
        this.Controls.Add(this.Naam2);

        //Hoogte van het speelbord
        this.Controls.Add(new Tekst("Hoogte:", new Point(200, 20)));
        this.Hoogte.MaxLength = 2;
        this.Hoogte.Width = 90;
        this.Controls.Add(this.Hoogte);

        //Breedte van het speelbord
        this.Controls.Add(new Tekst("Breedte:", new Point(200, 90)));
        this.Breedte.MaxLength = 2;
        this.Breedte.Width = 90;
        this.Controls.Add(this.Breedte);

        //Aanvinken of hints zijn toegestaan
        this.Hints.Text = "Hints";
        this.Hints.Font = new Font("Corbel", 14);
        this.Hints.Checked = true;
        this.Hints.Location = new Point(40, 155);
        this.Controls.Add(this.Hints);

        this.optieKnop.Click += this.klik;
        this.Controls.Add(this.optieKnop);

        this.resetKnop.Width = 90;
        this.resetKnop.Click += this.klik;
        this.Controls.Add(this.resetKnop);
    }

    private void klik(object sender, EventArgs e)
    {
        //Opties opslaan
        if (sender == this.optieKnop)
        {
            //Opties worden voor het spelScherm ingesteld
            this.spelScherm.HintsToegestaan = this.Hints.Checked;
            this.spelScherm.SpelerNamen[0] = this.Naam1.Text;
            this.spelScherm.SpelerNamen[1] = this.Naam2.Text;

            //Instellen van de dimensies van het speelbord
            int veldHoogte;
            int veldBreedte;
            try
            {
                veldHoogte = Convert.ToInt32(this.Hoogte.Text);
            }
            catch
            {
                veldHoogte = 6;
            }

            try
            {
                veldBreedte = Convert.ToInt32(this.Breedte.Text);
            }
            catch
            {
                veldBreedte = 6;
            }
            this.spelScherm.VeldHoogte = veldHoogte;
            this.spelScherm.VeldBreedte = veldBreedte;

            this.Close();
        }

        //Reset instellingen naar standaard waarden
        if (sender == this.resetKnop)
        {
            this.Naam1.Text = "Speler 1";
            this.Naam2.Text = "Speler 2";

            this.Breedte.Text = "6";
            this.Hoogte.Text = "6";

            this.Hints.Checked = true;
        }
    }
}