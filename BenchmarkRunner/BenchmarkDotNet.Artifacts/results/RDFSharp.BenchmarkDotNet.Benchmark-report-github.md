``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 5 3400G with Radeon Vega Graphics, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.202
  [Host]     : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT
  DefaultJob : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT


```
|        Method |               graph1 |               graph2 |     Mean |   Error |  StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |--------------------- |--------------------- |---------:|--------:|--------:|-----:|-------:|------:|------:|----------:|
| IntersectWith | https(...).com/ [30] | https(...).com/ [30] | 102.3 ns | 1.78 ns | 1.39 ns |    1 | 0.2333 |     - |     - |     488 B |
