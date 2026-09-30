# ts-converter-be

> **Archived.** The conversion logic now runs in the browser, so this API is no
> longer needed. See [ts-converter-fe](https://github.com/BaronAdam/ts-converter-fe)
> (`ts-converter/src/app/domain/converter/conversion.ts`).

Simple application to learn the basics of Azure Functions (.NET 10, isolated
worker): converts in-game time from American Truck Simulator and Euro Truck
Simulator 2 into real time.

## Conversion rates

In-game minutes that pass per real minute:

| Game | Where           | Rate |
| ---- | --------------- | ---- |
| ATS  | in a city       | 3    |
| ATS  | outside a city  | 20   |
| ETS  | in a city       | 3    |
| ETS  | mainland Europe | 19   |
| ETS  | United Kingdom  | 15   |
