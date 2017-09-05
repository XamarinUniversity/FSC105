#r "packages/FSharp.Data.2.3.0/lib/net40/FSharp.Data.dll"

open FSharp.Data
open System.Net

[<Measure>] type Kelvin
[<Measure>] type Celsius
[<Measure>] type Fahrenheit

let convertKelvintoCelsius (temp : float<Kelvin>) = 
    (temp / 1.0<Kelvin> - 273.) * 1.0<Celsius>

let convertKelvintoFahrenheit (temp : float<Kelvin>) = 
    (temp / 1.0<Kelvin> * 9. / 5. - 459.67) * 1.0<Fahrenheit>

// You will need to sign-up to openweathermap.org in order to obtain an API key. 
// Replace all occurrances of <apikey> with the value you get from there
let apiKey = "<apikey>"

let weatherForCityUrl city = 
    let encodedCity = WebUtility.UrlEncode(city)
    "http://api.openweathermap.org/data/2.5/weather?q=" + encodedCity + "&APPID=" + apiKey

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?q=Sydney&APPID=<apikey>">

let displayWeather city = 
    let info = Weather.Load(weatherForCityUrl city)

    // Change the following to convertKelvintoFahrenheit if thats your local UOM
    let convert = convertKelvintoCelsius

    let actualTemp = convert <| (float info.Main.Temp) * 1.0<Kelvin>
    let minTemp = convert <|(float info.Main.TempMin) * 1.0<Kelvin>
    let maxTemp = convert <|(float info.Main.TempMax) * 1.0<Kelvin>

    printfn "The forecast for %s today is %s" info.Name info.Weather.[0].Main 
    printfn "The details for today is %.1f with a Min: %.1f and Max: %.1f" (float actualTemp) (float minTemp) (float maxTemp) 
    printfn "The location is (%f, %f)" info.Coord.Lat info.Coord.Lon
 
displayWeather "New York"
