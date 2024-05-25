namespace RestauranteAtomo.model
{
  internal class Cliente
  {
    static int ultimo_id = 0;

    #region atributos
    private string nome;
    private string contato;
    private int id;
    // private Requisicao requisicao;

    public Cliente(string nome, string contato)
    {
      this.nome = nome;
      this.contato = contato;
      this.id = ultimo_id++;
    }
    #endregion

    #region propriedades
    public string Nome
    {
      get { return nome; }
      set { nome = value; }
    }

    public string Contato
    {
      get { return contato; }
      set { contato = value; }
    }

    // public Requisicao Requisicao
    // {
    //   get { return requisicao;}
    // }
    #endregion

    #region metodos

    // /// <summary>
    // /// cria uma nova requisicao com a quantidade de pessoas
    // /// especificada
    // /// </summary>
    // /// <param name="quantidadePessoas"></param>
    // public Requisicao fazerRequisicao(int quantidadePessoas)
    // {
    //   return new Requisicao(this, quantidadePessoas);
    //   // return requisicao;
    // }    

    public override string ToString()
    {
      return "Nome: " + this.nome + "; Contato: " + this.contato;
    }
    #endregion

    public override bool Equals(Object ob)
    {
      Cliente cliente = (Cliente) ob;
      // return this.nome == cliente.nome && this.contato == cliente.contato;
      return this.id == cliente.id;
    }
  }
}
