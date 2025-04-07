using System;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
    public Circulo(Objeto _paiRef, ref char _rotulo, Double _raio) : this(_paiRef, ref _rotulo, _raio, new Ponto4D(0.5, 0.5))
    {

    }

    public Circulo(Objeto _paiRef, ref char _rotulo, Double _raio, Ponto4D ptoDeslocamento) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.TriangleFan;
      PrimitivaTamanho = 5;
      
      for (int i = 0; i < 72; i++) {
        double angulo = (2 * 3.14 * i) / 72;
        double x = _raio * Math.Cos(angulo);
        double y = _raio * Math.Sin(angulo);

        Ponto4D ponto = new Ponto4D(x, y);
        base.PontosAdicionar(ponto);
        
      }

      Atualizar(ptoDeslocamento);
    }

    private void Atualizar(Ponto4D ptoDeslocamento)
    {
      base.ObjetoAtualizar();
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