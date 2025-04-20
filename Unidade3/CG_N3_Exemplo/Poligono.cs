using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace gcgcg
{
  public class Poligono : Objeto
  {
    public Poligono(Objeto _paiRef, ref char _rotulo, List<Ponto4D> pontosPoligono) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.LineLoop;
      PrimitivaTamanho = 1;
      base.pontosLista = pontosPoligono;
      Atualizar();
    }

    private Ponto4D NearestPoint(Ponto4D point) 
    {
      IDistanceStrategy distance = new Manhattan();
      
      return base.pontosLista
        .OrderBy(p => distance.Distance(p.X, p.Y, point.X, point.Y))
        .First();
    }

    public void MoveNearest(Ponto4D point) 
    {
      Ponto4D nearest = NearestPoint(point);
      nearest.X = point.X;
      nearest.Y = point.Y;
      Atualizar();
    }

    public void RemoveNearest(Ponto4D point)
    {
      Ponto4D nearest = NearestPoint(point);
      base.pontosLista.Remove(nearest);
      Atualizar();
    }

    private void Atualizar()
    {
      base.ObjetoAtualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      System.Console.WriteLine("__________________________________ \n");
      string retorno;
      retorno = "__ Objeto Poligono _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return retorno;
    }
#endif

  }
}
