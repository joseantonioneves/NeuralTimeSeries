# Rede Neural para séries temporais
## Finalidade

Este projeto demonstra o uso de Redes Neurais Artificiais (RNA) para previsão de séries temporais, especificamente para prever o volume de agendamentos ao longo do tempo com dados históricos. O código utiliza uma rede neural personalizada para treinar e prever valores futuros com base em dados reais de janeiro de 2019 a dezembro de 2020.

---

## Organização do Código

- O projeto principal é o **NeuralTimeSeries**, que contém a lógica de carregamento, normalização dos dados, treinamento e previsão usando uma rede neural.
- A implementação da rede neural está em uma biblioteca separada chamada **RedeNeural** (referenciada como `IA`).
- O ponto de entrada do programa é o arquivo NeuralTimeSeriesProgram.cs.

---

## Estrutura de Diretório

```plaintext
NeuralTimeSeries/
│
├── App.config
├── NeuralTimeSeries.csproj
├── NeuralTimeSeries.sln
├── NeuralTimeSeriesProgram.cs
├── Properties/
│   └── AssemblyInfo.cs
├── bin/
│   ├── Debug/
│   │   ├── NeuralTimeSeries.exe
│   │   ├── NeuralTimeSeries.exe.config
│   │   ├── NeuralTimeSeries.pdb
│   │   ├── RedeNeural.dll
│   │   └── RedeNeural.pdb
│   └── Release/
├── obj/
│   ├── Debug/
│   └── Release/
```

---

## Como Usar?

1. **Abra a solução** NeuralTimeSeries.sln no Visual Studio.
2. Certifique-se de que o projeto de biblioteca **RedeNeural** (referenciado como `IA`) está presente e compilado.
3. Execute o projeto **NeuralTimeSeries**.
4. O programa exibirá no console o processo de normalização dos dados, treinamento da rede neural e previsão do próximo valor da série temporal.

---

## Como Compilar

1. Abra o terminal na raiz do projeto ou utilize o Visual Studio.
2. Compile o projeto com o comando:

    ```sh
    msbuild NeuralTimeSeries.csproj
    ```

   Ou, no Visual Studio, pressione `Ctrl+Shift+B` para compilar a solução.

3. Os binários serão gerados na pasta Debug ou Release.

---

## Observações

- O projeto depende da biblioteca **RedeNeural** (`IA`). Certifique-se de que ela está corretamente referenciada e compilada.
- O código foi desenvolvido para .NET Framework 4.7.2, conforme especificado em NeuralTimeSeries.csproj.

---