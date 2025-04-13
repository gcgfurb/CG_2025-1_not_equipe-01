using System;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
    private double raio;
    private Ponto4D centro;

    public Ponto4D Centro
    {
      get { return centro; }
    }

    public double getRaio() {
      return raio;
    }

    public Circulo(Objeto _paiRef, ref char _rotulo, double _raio) 
      : this(_paiRef, ref _rotulo, _raio, new Ponto4D(0.5, 0.5)) { }

    public Circulo(Objeto _paiRef, ref char _rotulo, double _raio, Ponto4D ptoDeslocamento)
      : base(_paiRef, ref _rotulo)
    {
      raio = _raio;
      centro = ptoDeslocamento;
      PrimitivaTamanho = 5;
      Atualizar();
    }

    private void Atualizar()
    {
      base.PontosApagar();

      for (int i = 0; i < 72; i++)
      {
        double angulo = (2 * Math.PI * i) / 72;
        double x = raio * Math.Cos(angulo) + centro.X;
        double y = raio * Math.Sin(angulo) + centro.Y;
        base.PontosAdicionar(new Ponto4D(x, y));
      }

      base.ObjetoAtualizar();
    }

    public void MoverCentro(double dx, double dy)
    {
      centro.X += dx;
      centro.Y += dy;
      Atualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      System.Console.WriteLine("__________________________________ \n");
      string retorno;
      retorno = "__ Objeto Circulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return retorno;
    }
#endif
  }
}
