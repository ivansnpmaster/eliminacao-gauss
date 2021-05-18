using System;
					
public class Program
{
	public static void Main(string[] args)
	{
		double[,] sistema = new double[,]
		{
			{1, 2, -1, 0, -4},
			{0, -1, 1, -1, 0},
			{-2, -1, 4, 2, 7},
			{4, 3, 0, 1, -10}
		};

		Matriz A = new Matriz(sistema);
		var s = A.EliminacaoGaussPivo();

		string r = default;

		for (int i = 0; i < s.Length; i++)
			r += Math.Round(s[i], 2).ToString() + "\n";

		Console.WriteLine(r);
	}
}

public class Matriz
{
	int linhas;
	int colunas;
	double[,] dados;

	public Matriz(double[,] m)
	{
		dados = m;
		linhas = dados.GetLength(0);
		colunas = dados.GetLength(1);
	}

	public double[] EliminacaoGaussPivo()
	{
		Matriz ret = new Matriz(dados);

		for (int i = 0; i < ret.linhas - 1; i++)
		{
			// Pivoteamento
			int iMax = i;
			double vMax = Math.Abs(ret.dados[i, i]);

			for (int n = i + 1; n < ret.linhas; n++)
				if (Math.Abs(ret.dados[n, i]) > vMax)
				{
					iMax = n;
					vMax = Math.Abs(ret.dados[n, i]);
				}

			if (iMax != i)
				for (int n = 0; n < ret.colunas; n++)
					(ret.dados[i, n], ret.dados[iMax, n]) = (ret.dados[iMax, n], ret.dados[i, n]);

			double[] k = new double[ret.linhas - (i + 1)];

			for (int m = 0; m < k.Length; m++)
			{
				k[m] = dados[i + m + 1, i] / dados[i, i];

				// Colocando os zeros nas posições abaixo do item da coluna atual
				ret.dados[i + 1 + m, i] = 0;
			}

			// Iterando nas colunas das linhas seguintes
			for (int n = i + 1; n < ret.linhas; n++)
				for (int j = i + 1; j < ret.colunas; j++)
					ret.dados[n, j] = dados[n, j] - k[n - (i + 1)] * dados[i, j];
		}

		double[] s = new double[ret.linhas];

		for (int i = ret.linhas; i-- > 0;)
		{
			double v = default;

			for (int j = ret.colunas - 2; j >= i; j--)
			{
				if (i == ret.linhas - 1)
					v += ret.dados[i, i + 1] / ret.dados[i, i];
				else
				{
					if (j == i + 1)
					{
						v += s[j] * ret.dados[i, j];
						v = ret.dados[i, ret.colunas - 1] - v;
					}
					else if (j != i)
						v += s[j] * ret.dados[i, j];
					else
						v /= ret.dados[i, i];
				}
			}

			s[i] = v;
		}

		return s;
	}
}
