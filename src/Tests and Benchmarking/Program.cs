using System;

using BenchmarkDotNet;
using BenchmarkDotNet.Running;

using Benchmarking;

var b = BenchmarkRunner.Run<SseTesting>();