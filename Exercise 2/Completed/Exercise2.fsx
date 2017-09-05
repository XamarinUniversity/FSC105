#r "packages/FSharp.Data.2.3.0/lib/net40/FSharp.Data.dll"
open FSharp.Data

let wb = WorldBankData.GetDataContext()

let filteredIncomeList = 
    query { 
        for c in wb.Countries do
            where (not 
                <| System.Double.IsNaN(c.Indicators.``Income share held by highest 10%``.[2010]))
            select (c.Name, c.Indicators.``Income share held by highest 10%``.[2010])
    }

let MinIncomeShare = filteredIncomeList |> Seq.minBy snd
let MaxIncomeShare = filteredIncomeList |> Seq.maxBy snd
