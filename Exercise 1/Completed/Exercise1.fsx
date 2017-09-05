#r "packages/FSharp.Data.2.3.0/lib/net40/FSharp.Data.dll"

open FSharp.Data
open System.Net

// You will need to sign-up to openweathermap.org in order to obtain an API key. 
// Replace all occurrances of <apikey> with the value you get from there
let apiKey = "<apikey>"

let weatherForCityUrl city = 
    let encodedCity = WebUtility.UrlEncode(city)
    "http://api.openweathermap.org/data/2.5/weather?q=" + encodedCity + "&APPID=" + apiKey

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?q=Sydney&APPID=<apikey>">

let kelvinToCelcius k = 
    (float k) - 273.15

let kelvinToFarenheit k = 
    (float k) * 9. / 5. - 459.67

let conversion = kelvinToCelcius

let displayWeather city = 
    let info = Weather.Load(weatherForCityUrl city)
    printfn "The forecast for %s today is %s" info.Name info.Weather.[0].Main 
    printfn "The location is (%f, %f)" info.Coord.Lat info.Coord.Lon
    printfn "The tempurate forcast is %.1f with a Min: %.1f and Max: %.1f" (conversion info.Main.Temp) (conversion info.Main.TempMin) (conversion info.Main.TempMax) 

displayWeather "New York"

