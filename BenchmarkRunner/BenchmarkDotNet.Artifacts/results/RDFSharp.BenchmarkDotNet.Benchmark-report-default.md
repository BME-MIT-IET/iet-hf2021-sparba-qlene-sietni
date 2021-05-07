
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 5 3400G with Radeon Vega Graphics, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.202
  [Host]     : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT
  DefaultJob : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT


                     Method |     Mean |   Error |  StdDev | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
--------------------------- |---------:|--------:|--------:|-----:|--------:|------:|------:|----------:|
 QuerySearchByNamePredicate | 218.1 μs | 0.88 μs | 0.73 μs |    1 | 40.5273 |     - |     - |   83.2 KB |
