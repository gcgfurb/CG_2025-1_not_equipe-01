#define CG_DEBUG
#define CG_Gizmo      
#define CG_OpenGL      
// #define CG_OpenTK
// #define CG_DirectX      
// #define CG_Privado      

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Diagnostics;

//FIXME: padrão Singleton

namespace gcgcg
{
  public class Mundo : GameWindow
  {
    private static Objeto mundo = null;
    private char rotuloNovo = '?';
    private Objeto objetoSelecionado = null;

    private readonly float[] _vertices =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,   0.0f, 1.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,   1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,    0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,   1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,   1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,   0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly float[] _verticesBasic =
        {
             // Position          Normal
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
        };

         private readonly float[] _verticesNoLight =
        {
            // Position
            -0.5f, -0.5f, -0.5f, // Front face
             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f, -0.5f,  0.5f, // Back face
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f, // Left face
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f, // Right face
             0.5f,  0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

            -0.5f, -0.5f, -0.5f, // Bottom face
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f, // Top face
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };

         private Texture _diffuseMap;

        // The specular map is a black/white representation of how specular each part of the texture is.
        private Texture _specularMap;

        private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);

        private int _vertexBufferObject;

        private int _vaoModel;

        private int _vaoLamp;

        private Shader _lampShader;

        private Shader _lightingShader;

    private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(0.0f),
            new Vector3(0.0f),
             new Vector3(0.0f),
        };

#if CG_Gizmo
    private readonly float[] _sruEixos =
    {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f  /* Z+ */
    };

    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;


    // FPS
    private int frames = 0;
    private Stopwatch stopwatch = new();
#endif

    private Shader _shaderBranca;
    private Shader _shaderVermelha;
    private Shader _shaderVerde;
    private Shader _shaderAzul;
    private Shader _shaderCiano;
    private Shader _shaderMagenta;
    private Shader _shaderAmarela;

    private Camera _camera;

    private Vector2 _ultimaPosicaoMouse;
    private bool _primeiroMovimentoMouse = true;

    private Ponto objetoMenor;
    public int lighting_control = 2;

    Matrix4 lampMatrix;

    public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
    {
      mundo ??= new Objeto(null, ref rotuloNovo); //padrão Singleton
    }


    protected override void OnLoad()
        {

                      
      _shaderAzul = new Shader("Shaders/shader.vert", "Shaders/shaderAzul.frag");
      _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");

      _lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

  

            {
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                // All of the vertex attributes have been updated to now have a stride of 8 float sizes.
                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                // The texture coords have now been added too, remember we only have 2 coordinates as the texture is 2d,
                // so the size parameter should only be 2 for the texture coordinates.
                var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            }
            
            {
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                // The lamp shader should have its stride updated aswell, however we dont actually
                // use the texture coords for the lamp, so we dont need to add any extra attributes.
                var positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            }
            
            // Our two textures are loaded in from memory, you should head over and
            // check them out and compare them to the results.
            _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");
      
      
      
      
      #region Objeto: Cubo
      objetoMenor = new Ponto(mundo, ref rotuloNovo, new Ponto4D(0.0, 0.0, 0.0))
      {
        PrimitivaTamanho = 10,
        shaderCor = _shaderAzul
      };
      objetoMenor.MatrizTranslacaoXYZ(2, 0, 0);
      #endregion

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorState = CursorState.Grabbed;
        }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
       base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel);

      // The two textures need to be used, in this case we use the diffuse map as our 0th texture
      // and the specular map as our 1st texture.

      switch (lighting_control)
      {
        
        case 2:
          _diffuseMap.Use(TextureUnit.Texture0);
          _specularMap.Use(TextureUnit.Texture1);
          _lightingShader.Use();

          _lightingShader.SetMatrix4("model", Matrix4.Identity);
          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("viewPos", _camera.Position);

          // Here we specify to the shaders what textures they should refer to when we want to get the positions.
          _lightingShader.SetInt("material.diffuse", 0);
          _lightingShader.SetInt("material.specular", 1);
          _lightingShader.SetFloat("material.shininess", 32.0f);

          _lightingShader.SetVector3("light.position", _lightPos);
          _lightingShader.SetVector3("light.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("light.specular", new Vector3(1.0f));

          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          Matrix4 lampMatrix = Matrix4.Identity;
          lampMatrix *= Matrix4.CreateScale(0.2f);
          lampMatrix *= Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
          break;

          case 0:

         _lightingShader.Use();

            // Matrix4.Identity is used as the matrix, since we just want to draw it at 0, 0, 0
            _lightingShader.SetMatrix4("model", Matrix4.Identity);
            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
            _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            // Draw the lamp, this is mostly the same as for the model cube
            GL.BindVertexArray(_vaoLamp);

            _lampShader.Use();

             lampMatrix = Matrix4.CreateScale(0.2f); // We scale the lamp cube down a bit to make it less dominant
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

            _lampShader.SetMatrix4("model", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        
          break;
        case 1:

          _lightingShader.Use();

          _lightingShader.SetMatrix4("model", Matrix4.Identity);
          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
          _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
          _lightingShader.SetVector3("lightPos", _lightPos);
          _lightingShader.SetVector3("viewPos", _camera.Position);

          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          Matrix4 lampMatrix1 = Matrix4.CreateScale(0.2f);
          lampMatrix1 = lampMatrix1 * Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix1);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          break;
        case 3:

          _diffuseMap.Use(TextureUnit.Texture0);
          _specularMap.Use(TextureUnit.Texture1);
          _lightingShader.Use();

          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("viewPos", _camera.Position);

          _lightingShader.SetInt("material.diffuse", 0);
          _lightingShader.SetInt("material.specular", 1);
          _lightingShader.SetFloat("material.shininess", 32.0f);

          // Directional light needs a direction, in this example we just use (-0.2, -1.0, -0.3f) as the lights direction
          _lightingShader.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
          _lightingShader.SetVector3("light.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("light.specular", new Vector3(1.0f));

          // We want to draw all the cubes at their respective positions

          // Then we translate said matrix by the cube position
          Matrix4 model = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
          // We then calculate the angle and rotate the model around an axis
          float angle = 20.0f * 0;
          model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
          // Remember to set the model at last so it can be used by opentk
          _lightingShader.SetMatrix4("model", model);

          // At last we draw all our cubes
          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);



          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          lampMatrix = Matrix4.CreateScale(0.2f);
          lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
          break;
        case 4:

          _diffuseMap.Use(TextureUnit.Texture0);
          _specularMap.Use(TextureUnit.Texture1);
          _lightingShader.Use();

          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("viewPos", _camera.Position);

          _lightingShader.SetInt("material.diffuse", 0);
          _lightingShader.SetInt("material.specular", 1);
          _lightingShader.SetFloat("material.shininess", 32.0f);

          _lightingShader.SetVector3("light.position", _lightPos);
          _lightingShader.SetFloat("light.constant", 1.0f);
          _lightingShader.SetFloat("light.linear", 0.09f);
          _lightingShader.SetFloat("light.quadratic", 0.032f);
          _lightingShader.SetVector3("light.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("light.specular", new Vector3(1.0f));


          // First we create a model from an identity matrix
          // Then we translate said matrix by the cube position
          model = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
          // We then calculate the angle and rotate the model around an axis
          angle = 20.0f * 0;
          model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
          // Remember to set the model at last so it can be used by opentk
          _lightingShader.SetMatrix4("model", model);

          // At last we draw all our cubes
          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);


          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          lampMatrix = Matrix4.CreateScale(0.2f);
          lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
          break;
        case 5:
          _diffuseMap.Use(TextureUnit.Texture0);
          _specularMap.Use(TextureUnit.Texture1);
          _lightingShader.Use();

          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("viewPos", _camera.Position);

          _lightingShader.SetInt("material.diffuse", 0);
          _lightingShader.SetInt("material.specular", 1);
          _lightingShader.SetFloat("material.shininess", 32.0f);

          _lightingShader.SetVector3("light.position", _camera.Position);
          _lightingShader.SetVector3("light.direction", _camera.Front);
          _lightingShader.SetFloat("light.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
          _lightingShader.SetFloat("light.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
          _lightingShader.SetFloat("light.constant", 1.0f);
          _lightingShader.SetFloat("light.linear", 0.09f);
          _lightingShader.SetFloat("light.quadratic", 0.032f);
          _lightingShader.SetVector3("light.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("light.specular", new Vector3(1.0f));

          // We want to draw all the cubes at their respective positions

          // Then we translate said matrix by the cube position
          model = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
          // We then calculate the angle and rotate the model around an axis
          angle = 20.0f * 0;
          model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
          // Remember to set the model at last so it can be used by opentk
          _lightingShader.SetMatrix4("model", model);

          // At last we draw all our cubes
          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);


          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          lampMatrix = Matrix4.CreateScale(0.2f);
          lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          break;
        case 6:
           _diffuseMap.Use(TextureUnit.Texture0);
          _specularMap.Use(TextureUnit.Texture1);
          _lightingShader.Use();

          _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

          _lightingShader.SetVector3("viewPos", _camera.Position);

          _lightingShader.SetInt("material.diffuse", 0);
          _lightingShader.SetInt("material.specular", 1);
          _lightingShader.SetFloat("material.shininess", 32.0f);

          // Directional light needs a direction, in this example we just use (-0.2, -1.0, -0.3f) as the lights direction
          _lightingShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
          _lightingShader.SetVector3("dirLight.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("dirLight.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("dirLight.specular", new Vector3(1.0f));
          


          for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                _lightingShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _lightingShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _lightingShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _lightingShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _lightingShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _lightingShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _lightingShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }


          _lightingShader.SetVector3("spotLight.position", _camera.Position);
          _lightingShader.SetVector3("spotLight.direction", _camera.Front);
          _lightingShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
          _lightingShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
          _lightingShader.SetFloat("spotLight.constant", 1.0f);
          _lightingShader.SetFloat("spotLight.linear", 0.09f);
          _lightingShader.SetFloat("spotLight.quadratic", 0.032f);
          _lightingShader.SetVector3("spotLight.ambient", new Vector3(0.2f));
          _lightingShader.SetVector3("spotLight.diffuse", new Vector3(0.5f));
          _lightingShader.SetVector3("spotLight.specular", new Vector3(1.0f));

          // We want to draw all the cubes at their respective positions

          // Then we translate said matrix by the cube position
          model = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
          // We then calculate the angle and rotate the model around an axis
          angle = 20.0f * 0;
          model = model * Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
          // Remember to set the model at last so it can be used by opentk
          _lightingShader.SetMatrix4("model", model);

          // At last we draw all our cubes
          GL.DrawArrays(PrimitiveType.Triangles, 0, 36);



          GL.BindVertexArray(_vaoLamp);

          _lampShader.Use();

          lampMatrix = Matrix4.CreateScale(0.2f);
          lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

          _lampShader.SetMatrix4("model", lampMatrix);
          _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
          _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            
          break;
      }
            

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

      mundo.Desenhar(new Transformacao4D(), _camera);

#if CG_Gizmo      
      //Gizmo_Sru3D();

      frames++;
      if (stopwatch.ElapsedMilliseconds >= 1000)
      {
        Console.WriteLine($"FPS: {frames}");
        frames = 0;
        stopwatch.Restart();
      }
#endif
      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      // ☞ 396c2670-8ce0-4aff-86da-0f58cd8dcfdc   TODO: forma otimizada para teclado.
      #region Teclado
      var estadoTeclado = KeyboardState;
      if (estadoTeclado.IsKeyDown(Keys.Escape))
        Close();
      if (estadoTeclado.IsKeyPressed(Keys.Space))
      {
        if (objetoSelecionado == null)
          objetoSelecionado = mundo;

        objetoSelecionado.shaderCor = _shaderBranca;
        objetoSelecionado = mundo.GrafocenaBuscaProximo(objetoSelecionado);
        objetoSelecionado.shaderCor = _shaderAmarela;
      }
      if (estadoTeclado.IsKeyPressed(Keys.G))
        mundo.GrafocenaImprimir("");
      if (estadoTeclado.IsKeyPressed(Keys.P) && objetoSelecionado != null)
        Console.WriteLine(objetoSelecionado.ToString());
      if (estadoTeclado.IsKeyPressed(Keys.M) && objetoSelecionado != null)
        objetoSelecionado.MatrizImprimir();
      if (estadoTeclado.IsKeyPressed(Keys.I) && objetoSelecionado != null)
        objetoSelecionado.MatrizAtribuirIdentidade();
      if (estadoTeclado.IsKeyPressed(Keys.Left) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(-0.05, 0, 0);
      if (estadoTeclado.IsKeyPressed(Keys.Right) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0.05, 0, 0);
      if (estadoTeclado.IsKeyPressed(Keys.Up) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0.05, 0);
      if (estadoTeclado.IsKeyPressed(Keys.Down) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, -0.05, 0);
      if (estadoTeclado.IsKeyPressed(Keys.O) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0, 0.05);
      if (estadoTeclado.IsKeyPressed(Keys.L) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0, -0.05);
      if (estadoTeclado.IsKeyPressed(Keys.PageUp) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZ(2, 2, 2);
      if (estadoTeclado.IsKeyPressed(Keys.PageDown) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZ(0.5, 0.5, 0.5);
      if (estadoTeclado.IsKeyPressed(Keys.Home) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZBBox(0.5, 0.5, 0.5);
      if (estadoTeclado.IsKeyPressed(Keys.End) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZBBox(2, 2, 2);

      if (estadoTeclado.IsKeyPressed(Keys.D0))
      {
        garbageColector();
         GL.UseProgram(0);
        _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticesNoLight.Length * sizeof(float), _verticesNoLight, BufferUsageHint.StaticDraw);

            // Load the two different shaders, they use the same vertex shader program. However they have two different fragment shaders.
            // This is because the lamp only uses a basic shader to turn it white, it wouldn't make sense to have the lamp lit in other colors.
            // The lighting shaders uses the lighting.frag shader which is what a large part of this chapter will be about
            _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_no.frag");
            _lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            {
                // Initialize the vao for the model
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                var vertexLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(vertexLocation);
                GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            }

            {
                // Initialize the vao for the lamp, this is mostly the same as the code for the model cube
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                // Set the vertex attributes (only position data for our lamp)
                var vertexLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(vertexLocation);
                GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            }
        lighting_control = 0;
      }
      if (estadoTeclado.IsKeyPressed(Keys.D1))
      {
        garbageColector();
        GL.UseProgram(0);
        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_basic.frag");
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _verticesBasic.Length * sizeof(float), _verticesBasic, BufferUsageHint.StaticDraw);
        _vaoModel = GL.GenVertexArray();
        GL.BindVertexArray(_vaoModel);

        var positionLocation = _lightingShader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        // Remember to change the stride as we now have 6 floats per vertex
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

        // We now need to define the layout of the normal so the shader can use it
        var normalLocation = _lightingShader.GetAttribLocation("aNormal");
        GL.EnableVertexAttribArray(normalLocation);
        GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        _vaoLamp = GL.GenVertexArray();
        GL.BindVertexArray(_vaoLamp);

        positionLocation = _lampShader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        // Also change the stride here as we now have 6 floats per vertex. Now we don't define the normal for the lamp VAO
        // this is because it isn't used, it might seem like a waste to use the same VBO if they dont have the same data
        // The two cubes still use the same position, and since the position is already in the graphics memory it is actually
        // better to do it this way. Look through the web version for a much better understanding of this.
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

        lighting_control = 1;
      }
      if (estadoTeclado.IsKeyPressed(Keys.D2))
      {
        garbageColector();
        GL.UseProgram(0);
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");


        _vaoModel = GL.GenVertexArray();
        GL.BindVertexArray(_vaoModel);

        // All of the vertex attributes have been updated to now have a stride of 8 float sizes.
        var positionLocation = _lightingShader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

        var normalLocation = _lightingShader.GetAttribLocation("aNormal");
        GL.EnableVertexAttribArray(normalLocation);
        GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

        // The texture coords have now been added too, remember we only have 2 coordinates as the texture is 2d,
        // so the size parameter should only be 2 for the texture coordinates.
        var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
        GL.EnableVertexAttribArray(texCoordLocation);
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));



        _vaoLamp = GL.GenVertexArray();
        GL.BindVertexArray(_vaoLamp);

        // The lamp shader should have its stride updated aswell, however we dont actually
        // use the texture coords for the lamp, so we dont need to add any extra attributes.
        positionLocation = _lampShader.GetAttribLocation("aPos");
        GL.EnableVertexAttribArray(positionLocation);
        GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);


        // Our two textures are loaded in from memory, you should head over and
        // check them out and compare them to the results.
        _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");
        lighting_control = 2;
       }
      
      if (estadoTeclado.IsKeyPressed(Keys.D3))
      {
        garbageColector();
        GL.UseProgram(0);
         _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_directional.frag");
            
            
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            

            
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

                 positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            
            _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");

            
        lighting_control = 3;
      }

      if (estadoTeclado.IsKeyPressed(Keys.D4))
      {
        garbageColector();
        GL.UseProgram(0);
         _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_positional.frag");
            {
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                var positionLocation = _lightingShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = _lightingShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            }

            {
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                var positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            }
            _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");

        lighting_control = 4;
      }
      if (estadoTeclado.IsKeyPressed(Keys.D5))
      {
        garbageColector();
        GL.UseProgram(0);
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
          _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_spotlight.frag");
        {
          _vaoModel = GL.GenVertexArray();
          GL.BindVertexArray(_vaoModel);

          var positionLocation = _lightingShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

          var normalLocation = _lightingShader.GetAttribLocation("aNormal");
          GL.EnableVertexAttribArray(normalLocation);
          GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

          var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
          GL.EnableVertexAttribArray(texCoordLocation);
          GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        {
          _vaoLamp = GL.GenVertexArray();
          GL.BindVertexArray(_vaoLamp);

          var positionLocation = _lampShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        }
        _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");

        lighting_control = 5;
      }

      if (estadoTeclado.IsKeyPressed(Keys.D6))
      {
        garbageColector();
        GL.UseProgram(0);
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting_multiple.frag");

        
          _vaoModel = GL.GenVertexArray();
          GL.BindVertexArray(_vaoModel);

          var positionLocation = _lightingShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

          var normalLocation = _lightingShader.GetAttribLocation("aNormal");
          GL.EnableVertexAttribArray(normalLocation);
          GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

          var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
          GL.EnableVertexAttribArray(texCoordLocation);
          GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        

        
          _vaoLamp = GL.GenVertexArray();
          GL.BindVertexArray(_vaoLamp);

          positionLocation = _lampShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
          _diffuseMap = Texture.LoadFromFile("Resources/imagem_grupo.png");
            _specularMap = Texture.LoadFromFile("Resources/container2_specular.png");
        

        lighting_control = 6;
      }
      const float cameraSpeed = 1.5f;
      if (estadoTeclado.IsKeyDown(Keys.Z))
        _camera.Position = Vector3.UnitZ * 5;
      if (estadoTeclado.IsKeyDown(Keys.W))
        _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
      if (estadoTeclado.IsKeyDown(Keys.S))
        _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
      if (estadoTeclado.IsKeyDown(Keys.A))
        _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
      if (estadoTeclado.IsKeyDown(Keys.D))
        _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
      if (estadoTeclado.IsKeyDown(Keys.RightShift))
        _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
      if (estadoTeclado.IsKeyDown(Keys.LeftShift))
        _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
                                                                      // if (estadoTeclado.IsKeyDown(Keys.D9))
                                                                      //   _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
                                                                      // if (estadoTeclado.IsKeyDown(Keys.D0))
                                                                      //   _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down

      #endregion

      #region  Mouse

      if (MouseState.IsButtonDown(MouseButton.Left))
      {
        var mouse = MouseState.Position;

        if (_primeiroMovimentoMouse)
        {
          _ultimaPosicaoMouse = mouse;
          _primeiroMovimentoMouse = false;
        }

        var deltaX = mouse.X - _ultimaPosicaoMouse.X;
        var deltaY = _ultimaPosicaoMouse.Y - mouse.Y; // Inverter Y (sistema de coordenadas de tela)

        _ultimaPosicaoMouse = mouse;
        float sensibilidade = 0.2f;
        _camera.Yaw += deltaX * sensibilidade;
        _camera.Pitch += deltaY * sensibilidade;

        _camera.Pitch = Math.Clamp(_camera.Pitch, -89f, 89f);

      }
      else
      {
        _primeiroMovimentoMouse = true;
      }

      if (MouseState.IsButtonDown(MouseButton.Right) && objetoSelecionado != null)
      {
        Console.WriteLine("MouseState.IsButtonDown(MouseButton.Right)");

        int janelaLargura = ClientSize.X;
        int janelaAltura = ClientSize.Y;
        Ponto4D mousePonto = new Ponto4D(MousePosition.X, MousePosition.Y);
        Ponto4D sruPonto = Utilitario.NDC_TelaSRU(janelaLargura, janelaAltura, mousePonto);

        objetoSelecionado.PontosAlterar(sruPonto, 0);
      }
      if (MouseState.IsButtonReleased(MouseButton.Right))
      {
        Console.WriteLine("MouseState.IsButtonReleased(MouseButton.Right)");
      }

      objetoMenor.MatrizRotacao(0.05f);

      #endregion
    }

    protected void garbageColector()
    {
       GL.DeleteProgram(_lightingShader.Handle);
       GL.DeleteProgram(_lampShader.Handle);

      // Delete VAOs and VBOs
      GL.DeleteVertexArray(_vaoModel);
      GL.DeleteVertexArray(_vaoLamp);
      GL.DeleteBuffer(_vertexBufferObject);

      // Delete textures
      GL.DeleteTexture(_diffuseMap.Handle);
      GL.DeleteTexture(_specularMap.Handle);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

#if CG_DEBUG      
      Console.WriteLine("Tamanho interno da janela de desenho: " + ClientSize.X + "x" + ClientSize.Y);
#endif
      GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    protected override void OnUnload()
    {
      mundo.OnUnload();

      garbageColector();


      base.OnUnload();
    }

#if CG_Gizmo
    private void Gizmo_Sru3D()
    {
#if CG_OpenGL && !CG_DirectX
      var model = Matrix4.Identity;
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      // EixoX
      _shaderVermelha.SetMatrix4("model", model);
      _shaderVermelha.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderVermelha.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderVermelha.Use();
      GL.DrawArrays(PrimitiveType.Lines, 0, 2);
      // EixoY
      _shaderVerde.SetMatrix4("model", model);
      _shaderVerde.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderVerde.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderVerde.Use();
      GL.DrawArrays(PrimitiveType.Lines, 2, 2);
      // EixoZ
      _shaderAzul.SetMatrix4("model", model);
      _shaderAzul.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderAzul.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderAzul.Use();
      GL.DrawArrays(PrimitiveType.Lines, 4, 2);
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
    }
#endif    

  }
}
