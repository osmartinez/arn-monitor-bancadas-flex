using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impresion
{
    public abstract class Imprime
    {
        // contiene el texto a escribir
        public string Text { get; set; }

        // coordenadas rectángulo a escribir
        public float X { get; set; }
        public float Y { get; set; }

        public float XCb { get; set; }
        public float YCb { get; set; }
        // dimensiones página
        public const float WIDTH = 285.0F;
        public const float HEIGHT = 50 * 5F;

        // dimensiones por bloques
        public float WidthSeccionDerecha { get; set; }
        public float Division1 { get; set; }
        public float Division2 { get; set; }
        public float Division3 { get; set; }
        public float Division4 { get; set; }

        // fuentes y colores
        public Font Lucida12negrita { get; set; } = new Font("Lucida Console", 12, FontStyle.Bold);
        public Font Lucida9 { get; set; } = new System.Drawing.Font("Lucida Console", 9, FontStyle.Regular);
        public Font Lucida10 { get; set; } = new Font("Lucida Console", 10, FontStyle.Regular);
        public Font Lucida11 { get; set; } = new Font("Lucida Console", 11, FontStyle.Regular);
        public Font Lucida12 { get; set; } = new Font("Lucida Console", 12, FontStyle.Regular);
        public Font Lucida14 { get; set; } = new Font("Lucida Console", 14, FontStyle.Regular);
        public Font Lucida16 { get; set; } = new Font("Lucida Console", 16, FontStyle.Regular);
        public Font Lucida22 { get; set; } = new Font("Lucida Console", 22, FontStyle.Regular);
        public Font Arial12negrita { get; set; } = new Font("Arial", 12, FontStyle.Bold);
        public Font Arial8 { get; set; } = new Font("Arial", 8, FontStyle.Regular);
        public Font Arial10negrita { get; set; } = new Font("Arial", 10, FontStyle.Bold);
        public Font Arial08 { get; set; } = new Font("Arial", 8, FontStyle.Regular);
        public Font Arial10 { get; set; } = new Font("Arial", 10, FontStyle.Regular);
        public Font Arial12 { get; set; } = new Font("Arial", 12, FontStyle.Regular);
        public Font Arial14 { get; set; } = new Font("Arial", 14, FontStyle.Regular);
        public Font Arial16 { get; set; } = new Font("Arial", 16, FontStyle.Regular);
        public Font Arial18 { get; set; } = new Font("Arial", 18, FontStyle.Regular);
        public Font Arial22 { get; set; } = new Font("Arial", 22, FontStyle.Regular);
        public Font Arial22negrita { get; set; } = new Font("Arial", 22, FontStyle.Bold);
        public Font Arial24 { get; set; } = new Font("Arial", 24, FontStyle.Regular);
        public Font Arial24negrita { get; set; } = new Font("Arial", 24, FontStyle.Bold);

        public Font Arial26 { get; set; } = new Font("Arial", 26, FontStyle.Regular);
        public Font Arial32 { get; set; } = new Font("Arial", 32, FontStyle.Regular);
        public Font Arial28 { get; set; } = new Font("Arial", 28, FontStyle.Regular);

        public SolidBrush DrawBrush = new SolidBrush(Color.Black);
        public SolidBrush DrawWhiteBrush = new SolidBrush(Color.White);


        // contador etiquetas
        public int Cuanto { get; set; }

        // Set format of string.
        public const StringAlignment Center = StringAlignment.Center;
        public const StringAlignment Left = StringAlignment.Near;
        public const StringAlignment Right = StringAlignment.Far;

        public StringFormat DrawFormatCenter { get; private set; }
        public StringFormat DrawFormatLeft { get; private set; }
        public StringFormat DrawFormatRight { get; private set; }

        // nombre impresora y codigo de barras
        public string PrinterName { get; private set; }
        public string CodigoBarras { get; set; }
        public int DesplazamientoCB { get; private set; }


        public Imprime(string nombreImpresora)
        {
            // recoge parámetros y establace contadores
            this.PrinterName = nombreImpresora;
            this.CodigoBarras = "";
            this.Cuanto = 0;

            // establece las divisiones
            this.Division1 = WIDTH * 0.25f;
            this.Division2 = WIDTH * 0.50f;
            this.Division3 = WIDTH * 0.75f + 20;
            this.Division4 = WIDTH;
            this.WidthSeccionDerecha = Division1 + 30;

            // instancia stringFormat
            this.DrawFormatCenter = new StringFormat() { Alignment = StringAlignment.Center };
            this.DrawFormatLeft = new StringFormat() { Alignment = StringAlignment.Near };
            this.DrawFormatRight = new StringFormat() { Alignment = StringAlignment.Far };

            // establece desplazamientos
            this.DesplazamientoCB = 140;
            this.YCb = 10;
            this.XCb = 10;

        }

        public abstract void Imprimir();

        public virtual void DibujarBloque(PrintPageEventArgs e, Font fuente, StringAlignment alineacion = Imprime.Center, float division = Imprime.WIDTH, bool saltoLinea = true)
        {
            e.Graphics.DrawString(this.Text, fuente, this.DrawBrush,
                new RectangleF(this.X, this.Y, division, Imprime.HEIGHT),
                new StringFormat() { Alignment = alineacion });
            if (saltoLinea)
            {
                float ancho = e.Graphics.MeasureString(this.Text, fuente).Width;
                if (ancho > this.X + division && alineacion == StringAlignment.Near)
                {
                    this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height * 2 + 5;
                }
                else
                {
                    this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height + 5;
                }

                if (this.Text.Length > 24)
                {
                    if (ancho > this.X + division && alineacion == StringAlignment.Near)
                    {
                        this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height * 2 + 5;
                    }
                    else
                    {
                        this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height + 5;
                    }
                }
            }


        }

        public virtual void DibujarBloqueNegativo(PrintPageEventArgs e, Font fuente, StringAlignment alineacion = Imprime.Center, float division = Imprime.WIDTH, bool saltoLinea = true)
        {
            RectangleF rect = new RectangleF(this.X, this.Y, division, fuente.Height + 2);
            e.Graphics.FillRectangle(Brushes.Black, rect);
            e.Graphics.DrawString(this.Text, fuente, this.DrawWhiteBrush,
                rect,
                new StringFormat() { Alignment = alineacion });
            if (saltoLinea)
            {
                float ancho = e.Graphics.MeasureString(this.Text, fuente).Width;
                if (ancho > this.X + division && alineacion == StringAlignment.Near)
                {
                    this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height * 2 + 5;
                }
                else
                {
                    this.Y += (int)e.Graphics.MeasureString(this.Text, fuente).Height + 5;
                }
            }
        }

        public virtual void DibujarLinea(PrintPageEventArgs e, float x1, float y1, float x2, float y2)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
        }


        public void GenerarCB(string code, PrintPageEventArgs ev)
        {
            //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            //b.IncludeLabel = true;
            //b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
            //Image img = b.Encode(BarcodeLib.TYPE.CODE128, code, Color.Black, Color.White, 260, 80);

            //ev.Graphics.DrawImage(img, new PointF(this.XCb, this.YCb));
        }

        //public EAN13 CreateEAN13(string bc)
        //{
        //    EAN13 ean13 = new EAN13();
        //    ean13.CountryCode = bc.Substring(0, 2);
        //    ean13.ManufacturerCode = bc.Substring(2, 5);
        //    ean13.ProductCode = bc.Substring(7, 5);
        //    return ean13;
        //}

        //public void GenerarCB(PrintPageEventArgs ev)
        //{
        //    EAN13 ean13 = CreateEAN13(this.CodigoBarras);
        //    ean13.Scale = 1.2f;
        //    System.Drawing.Bitmap bmp = ean13.CreateBitmap();

        //    ean13.DrawEan13Barcode(ev.Graphics, new System.Drawing.Point((int)this.XCb, (int)this.YCb));
        //}
    }

}
