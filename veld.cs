using System;
using System.Windows.Forms;
using System.Drawing;

class Veld : UserControl
{
    public int veldBreedte, veldHoogte, vakGrootte;
    int[,] geheugen;
    bool[,] geldig;
    public bool hint = false;
    bool gestart = false;
    bool[] pas = new bool[2];
    public int beurt = 0;
    public int mogelijkheden
    {
        get
        {
            int mogelijkheden = 0;
            for (int y = 0; y < this.veldHoogte; y++)
                for (int x = 0; x < this.veldBreedte; x++)
                    if (this.geldig[x, y]) mogelijkheden++;

            return mogelijkheden;
        }
    }
    public static Color speler1Kleur = Color.White;
    public static Color speler2Kleur = Color.Black;
    public Brush speler2Brush = new SolidBrush(speler1Kleur);
    public Brush speler1Brush = new SolidBrush(speler2Kleur);

    public Veld(Point locatie, int veldBreedte, int veldHoogte)
    {
        //Minimum breedte en hoogte is 3
        if (veldBreedte < 3) veldBreedte = 3;
        if (veldHoogte < 3) veldHoogte = 3;

        this.veldBreedte = veldBreedte;
        this.veldHoogte = veldHoogte;
        this.vakGrootte = 50;

        //User control eigenschappen
        this.Height = this.vakGrootte * this.veldHoogte + 1;
        this.Width = this.vakGrootte * this.veldBreedte + 1;
        this.Location = locatie;
        this.BackColor = Color.Green;
        this.Paint += this.tekenVeld;
        this.MouseClick += this.doeZet;

        this.initVeld();
    }

    public void initVeld()
    {
        //Reset het geheugen
        this.geheugen = new int[this.veldBreedte, this.veldHoogte];        
    }

    public void startSpel()
    {
        this.beurt = 1;
        this.gestart = true;

        //Plaats 4 stenen in het midden van het veld
        int middenX = (int)Math.Ceiling((double)this.veldBreedte / 2);
        int middenY = (int)Math.Ceiling((double)this.veldHoogte / 2);
        this.geheugen[middenX - 1, middenY - 1] = 1;
        this.geheugen[middenX, middenY] = 1;
        this.geheugen[middenX, middenY - 1] = 2;
        this.geheugen[middenX - 1, middenY] = 2;

        this.Cursor = Cursors.Hand;
    }

    private void tekenVeld(object obj, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //Reset de array met geldige zetten
        this.geldig = new bool[this.veldBreedte, this.veldHoogte];

        //Teken de stenen uit het geheugen op het bord
        this.tekenVakjes(gr);

        //Debug info speelVeld
        Console.WriteLine("Aan de beurt: {4}, Beurt: {0}, Mogelijkheden: {1}, Speler 1 Score: {2}, Speler 2 Score: {3}", this.beurt, this.mogelijkheden, this.score(1), this.score(2), this.beurt % 2);

        if (mogelijkheden == 0 && gestart)
        {
            this.pas[this.beurt % 2] = true;
            
            if (this.pas[0] && this.pas[1])
            {
                this.eindeSpel();
            }
            else
            {
                if (this.pas[0]) MessageBox.Show("Speler 1 heeft geen mogelijke zetten. Speler 2 is aan de beurt.");
                else if (this.pas[1]) MessageBox.Show("Speler 2 heeft geen mogelijke zetten. Speler 1 is aan de beurt.");
                this.Invalidate();
                this.beurt++;
            }
        }

        this.hint = false;
    }

    //Geef de score van de aangegeven speler aan de hand van het geheugen
    public int score(int speler)
    {
        int score = 0;
        for (int y = 0; y < this.veldHoogte; y++)
            for (int x = 0; x < this.veldBreedte; x++)
                if (this.geheugen[x, y] == speler) score++;

        return score;
    }

    private void eindeSpel()
    {
        String message = "Er zijn geen mogelijke zetten meer.";
        if (score(1) > score(2)) message += " Speler 1 wint!";
        else if (score(1) < score(2)) message += " Speler 2 wint!";
        else message += " Remise!";
        this.gestart = false;
        MessageBox.Show(message);
    }

    //Teken het veld vanuit het geheugen op het scherm
    private void tekenVakjes(Graphics gr)
    {
        Brush kleur = default(Brush);

        for (int y = 0; y < this.veldHoogte; y++)
            for (int x = 0; x < this.veldBreedte; x++)
            {
                //Teken vakjes
                gr.DrawRectangle(Pens.Black, this.vakGrootte * x, this.vakGrootte * y, this.vakGrootte, this.vakGrootte);

                //Bepaal kleur van steen en teken steen
                if (this.geheugen[x, y] > 0)
                {
                    if (this.geheugen[x, y] == 1) kleur = this.speler1Brush;
                    else if (this.geheugen[x, y] == 2) kleur = this.speler2Brush;
                    gr.FillEllipse(kleur, 5 + x * this.vakGrootte, 5 + y * this.vakGrootte, this.vakGrootte - 10, this.vakGrootte - 10);
                }

                //Teken mogelijke zetten
                else if (this.valideerZet(x, y) && hint)
                {
                    this.geldig[x, y] = true;
                    Pen pen = default(Pen);
                    if (this.beurt % 2 == 0) pen = new Pen(this.speler1Brush);
                    else if (this.beurt % 2 == 1) pen = new Pen(this.speler2Brush);
                    gr.DrawEllipse(pen, 5 + x * this.vakGrootte, 5 + y * this.vakGrootte, this.vakGrootte - 10, this.vakGrootte - 10);
                }
                else if (this.valideerZet(x, y))
                {
                    this.geldig[x, y] = true;
                }
            }
    }

    private void doeZet(object obj, MouseEventArgs m)
    {
        this.pas[this.beurt % 2] = false;

        //Bepaal in welk vak is geklikt
        int vakX = m.X / 50;
        int vakY = m.Y / 50;

        if (this.valideerZet(vakX, vakY)) {
            //Verander het geheugen op de plaatsen van de veroverde stenen
            this.verover(vakX, vakY);

            //Plaats nieuwe steen in het geheugen
            this.geheugen[vakX, vakY] = this.beurt % 2 + 1;

            //Verander beurt
            this.beurt++;

            //Teken veld opnieuw
            this.Invalidate();
        }
    }

    //Controleer of zet geldig is
    private bool valideerZet(int x, int y)
    {
        if (!isBinnenVeld(x, y)) return false;
        
        if (this.heeftSteen(x,y)) return false;

        if (!this.check(x, y)) return false;

        return true;
    }

    //Check of zet binnen het veld is
    private bool isBinnenVeld(int x, int y)
    {
        if (x < 0 || x >= this.veldBreedte || y < 0 || y >= this.veldHoogte) return false;

        return true;
    }

    //Check of veld leeg is
    private bool heeftSteen(int x, int y)
    {
        if (this.geheugen[x, y] != 0) return true;

        return false;
    }

    //Check of de steen in het veld een andere is dan die van degene die aan de beurt is
    private bool isAndereSteen(int x, int y)
    {
        if ((this.geheugen[x, y] != this.beurt % 2 + 1) && (this.geheugen[x, y] > 0)) return true;

        return false;
    }

    //Check of de steen in het veld dezelfde is als die van degene die aan de beurt is
    private bool isZelfdeSteen(int x, int y)
    {
        if (this.geheugen[x, y] == this.beurt % 2 + 1) return true;
        return false;
    }

    //Check of de zet een tegel kan veroveren die ernaast zit
    private bool check(int x, int y)
    {
        if (this.checkRichting(x, y, 0, -1)) return true; //Boven
        if (this.checkRichting(x, y, 1, -1)) return true; //Rechtsboven
        if (this.checkRichting(x, y, 1, 0)) return true; //Rechts
        if (this.checkRichting(x, y, 1, 1)) return true; //Rechtsonder
        if (this.checkRichting(x, y, 0, 1)) return true; //Onder
        if (this.checkRichting(x, y, -1, 1)) return true; //Linksonder
        if (this.checkRichting(x, y, -1, 0)) return true; //Links
        if (this.checkRichting(x, y, -1, -1)) return true; //Linksboven

        else return false;
    }

    //Kijk welke stenen veroverd worden en verander het geheugen
    private void verover(int x, int y)
    {
        this.veroverRichting(x, y, 0, -1); //Boven
        this.veroverRichting(x, y, 1, -1); //Rechtsboven
        this.veroverRichting(x, y, 1, 0); //Rechts
        this.veroverRichting(x, y, 1, 1); //Rechtsonder
        this.veroverRichting(x, y, 0, 1); //Onder
        this.veroverRichting(x, y, -1, 1); //Linksonder
        this.veroverRichting(x, y, -1, 0); //Links
        this.veroverRichting(x, y, -1, -1); //Linksboven
    }

    //Check welke stenen in één van de acht richtingen veroverd mogen worden
    private void veroverRichting(int x, int y, int rx, int ry, bool volgende = false)
    {
        bool[,] veroverbaar = new bool[this.veldBreedte, this.veldHoogte];
        
        //Check of volgende stenen in deze richting hetzelfde is
        while (isBinnenVeld(x + rx, y + ry) && isAndereSteen(x + rx, y + ry))
        {
            x = x + rx;
            y = y + ry;
            veroverbaar[x, y] = true;
            volgende = true;
        }

        /*
         * Als er een andere steen is gevonden in deze richting (volgende == true)
         * en de laatst gecheckte steen was er één van degene die aan de beurt is,
         * wordt het geheugen veranderd op alle met 'veroverbaar' aangegeven plekken
         * aangepast
         */
        if (volgende && isBinnenVeld(x + rx, y + ry) && isZelfdeSteen(x + rx, y + ry))
        {
            for (int vy = 0; vy < this.veldHoogte; vy++)
                for (int vx = 0; vx < this.veldBreedte; vx++)
                    if (veroverbaar[vx, vy])
                    {
                        Console.WriteLine("vx {0}, vy {1} is veroverd", vx, vy);
                        this.geheugen[vx, vy] = this.beurt % 2 + 1;
                    }
            Console.WriteLine("Richting rx: {0}, ry: {1} is veroverd", rx, ry);
        }
    }

    //Check of in één van de acht richtingen een steen te veroveren is
    private bool checkRichting(int x, int y, int rx, int ry, bool volgende = false)
    {
        if (isBinnenVeld(x + rx, y + ry))
            /*
             * Check of volgende steen in deze richting hetzelfde is
             * Als dat zo is wordt de methode opnieuw aangeroepen totdat
             * een steen van de speler die de zet doet gevonden is
             */
            if (isAndereSteen(x + rx, y + ry))
                return checkRichting(x + rx, y + ry, rx, ry, true);

            /*Als er al een andere steen in deze richting is gevonden,
             * mag gekeken worden of de volgende steen hetzelfde is
             * en dus een insluitmogelijkheid vormt
             */
            else if (volgende && isZelfdeSteen(x + rx, y + ry)) return true;
        
        return false;
    }
}