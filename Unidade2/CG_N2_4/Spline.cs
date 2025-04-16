using System.Collections.Generic;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Spline : Objeto
  {

    public int ptoSelecionado = 0;
    private char rotuloAtual;
    private readonly int splineQtdPtoMax = 10;
    private int splineQtdPto = 10;

    private readonly List<Ponto> pontos;

    private readonly List<Ponto4D> pontos4D = [];
    private readonly List<SegReta> segRetas;
    private readonly List<SegReta> segRetas4D = [];

    private readonly Shader branco = new ("Shaders/shader.vert", "Shaders/shaderBranca.frag");
    private readonly Shader vermelho = new("Shaders/shader.vert", "Shaders/shaderVermelha.frag");
    private readonly Shader ciano = new("Shaders/shader.vert", "Shaders/shaderCiano.frag");
    private readonly Shader amarelo = new("Shaders/shader.vert", "Shaders/shaderAmarela.frag");

    public Spline(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Lines;
      PrimitivaTamanho = 10;

      pontos = [
        new (this, ref rotuloAtual, new(-0.5, -0.5)),
        new (this, ref rotuloAtual, new(-0.5, 0.5)),
        new (this, ref rotuloAtual, new(0.5, 0.5)),
        new (this, ref rotuloAtual, new(0.5, -0.5))
      ];

      segRetas = [
        new(this, ref rotuloAtual, pontos[0].PontosId(0), pontos[1].PontosId(0)),
        new(this, ref rotuloAtual, pontos[1].PontosId(0), pontos[2].PontosId(0)),
        new(this, ref rotuloAtual, pontos[2].PontosId(0), pontos[3].PontosId(0))
      ];

      segRetas.ForEach(delegate(SegReta s) { 
        s.ShaderObjeto = ciano; 
      });

      AtualizarSpline();
      PontoControler();
      PoligonoControler();
    }

    public void Atualizar()
    {
      base.ObjetoAtualizar();
    }

    public void AtualizarSpline()
    {
      pontos4D.Clear();
      segRetas4D.ForEach(delegate(SegReta s) {
        s.ObjetoRemover();
      });

      for (int i = 0; i < splineQtdPto; i++)
      {
        double t = i / (double)(splineQtdPto - 1);
        pontos4D.Add(CalcularBezier(pontos[0].PontosId(0), pontos[1].PontosId(0), pontos[2].PontosId(0), pontos[3].PontosId(0), t));
      }

      for (int j = 0; j < pontos4D.Count - 1; j++)
      {
        SegReta reta = new(this, ref rotuloAtual, pontos4D[j], pontos4D[j + 1])
        {
          ShaderObjeto = amarelo
        };
        segRetas4D.Add(reta);
      }
    }

    private static Ponto4D CalcularBezier(Ponto4D p0, Ponto4D p1, Ponto4D p2, Ponto4D p3, double t)
    {
        double u = 1 - t;
        double tt = t * t;
        double uu = u * u;
        double uuu = uu * u;
        double ttt = tt * t;

        double x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
        double y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

        return new Ponto4D(x, y);
    }

    private void PontoControler() 
    {
      pontos.ForEach(delegate(Ponto p) {
        p.ShaderObjeto = branco;
      });

      pontos[ptoSelecionado].ShaderObjeto = vermelho;
    }

    public void PoligonoControler()
    {
      segRetas.ForEach(delegate(SegReta s) { 
        s.ObjetoAtualizar(); 
      });
    }

    public void SplineQtdPonto(int inc)
    {
      splineQtdPto += inc;

      if (splineQtdPto < 2)
        splineQtdPto = 2;
      else if (splineQtdPto > splineQtdPtoMax)
        splineQtdPto = 10;
      AtualizarSpline();
    }

    public void MoverDireita()
    {
      pontos[ptoSelecionado].PontosId(0).X += 0.1;
      OnUpdate();
    }

    public void MoverEsquerda()
    {
      pontos[ptoSelecionado].PontosId(0).X -= 0.1;
      OnUpdate();
    }

    public void MoverCima()
    {
      pontos[ptoSelecionado].PontosId(0).Y += 0.1;
      OnUpdate();
    }

    public void MoverBaixo()
    {
      pontos[ptoSelecionado].PontosId(0).Y -= 0.1;
      OnUpdate();
    }

    private void OnUpdate()
    {
      pontos[ptoSelecionado].Atualizar();
      PoligonoControler();
      AtualizarSpline();
    }

    public void Next()
    {
      ptoSelecionado = (ptoSelecionado + 1) % pontos.Count;
      PontoControler();
    }

#if CG_Debug
    public override string ToString()
    {
      System.Console.WriteLine("__________________________________ \n");
      string retorno;
      retorno = "__ Objeto Retangulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return retorno;
    }
#endif

  }
}
