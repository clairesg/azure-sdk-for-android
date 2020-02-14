﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Xunit;
using Verifier = Azure.ClientSdk.Analyzers.Tests.AzureAnalyzerVerifier<Azure.ClientSdk.Analyzers.AddConfigureAwaitAnalyzer>;

namespace Azure.ClientSdk.Analyzers.Tests 
{
    public class AZC0012Tests 
    {
        [Theory]
        [InlineData(LanguageVersion.CSharp7)]
        [InlineData(LanguageVersion.Latest)]
        public async Task AZC0012WarningOnTask(LanguageVersion languageVersion)
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
            [|await System.Threading.Tasks.Task.Run(() => {})|];
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012", languageVersion);
        }

        [Theory]
        [InlineData(LanguageVersion.CSharp7)]
        [InlineData(LanguageVersion.Latest)]
        public async Task AZC0012WarningOnTaskOfT(LanguageVersion languageVersion) 
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
            var i = [|await System.Threading.Tasks.Task.Run(() => 42)|];
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012", languageVersion);
        }

        [Theory]
        [InlineData(LanguageVersion.CSharp7)]
        [InlineData(LanguageVersion.Latest)]
        public async Task AZC0012WarningOnValueTask(LanguageVersion languageVersion) 
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        private static int _x;

        public static async System.Threading.Tasks.ValueTask Foo()
        {
            [|await RunAsync()|];
        }

        private static async System.Threading.Tasks.ValueTask RunAsync()
        {
            _x++;
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012", languageVersion);
        }

        [Theory]
        [InlineData(LanguageVersion.CSharp7)]
        [InlineData(LanguageVersion.Latest)]
        public async Task AZC0012WarningOnValueTaskOfT(LanguageVersion languageVersion) 
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.ValueTask Foo()
        {
            var i = [|await GetValueAsync()|];
        }

        private static async System.Threading.Tasks.ValueTask<int> GetValueAsync()
        {
            return 0;
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012", languageVersion);
        }

        [Fact]
        public async Task AZC0012NoWarningOnExistingConfigureAwaitFalse()
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
           await System.Threading.Tasks.Task.Run(() => {}).ConfigureAwait(false);
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012WarningOnTaskDelay()
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
            [|await System.Threading.Tasks.Task.Delay(42)|];
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012NoWarningOnTaskYield()
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
           await System.Threading.Tasks.Task.Yield();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnNested()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task CallFooAsync()
        {
            await FooAsync(await (getFoo()).ConfigureAwait(false)).ConfigureAwait(false);
        }

        private static async Task FooAsync(bool foo) {}
        private static async Task<bool> getFoo() => true;
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012WarningOnVariable()
        {
            const string code = @"
namespace RandomNamespace
{
    public class MyClass
    {
        public static async System.Threading.Tasks.Task Foo()
        {
            var task = System.Threading.Tasks.Task.Run(() => {});
            [|await task|];
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncForeach()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            await foreach (var x in [|GetValuesAsync()|]) { }
        }

        private static async IAsyncEnumerable<int> GetValuesAsync() { yield break; }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012NoWarningOnAsyncForeachExistingConfigureAwaitFalse()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            await foreach (var x in GetValuesAsync().ConfigureAwait(false)) { }
        }

        private static async IAsyncEnumerable<int> GetValuesAsync() { yield break; }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnForeach()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            foreach (var x in GetValuesAsync()) { }
        }

        private static IEnumerable<int> GetValuesAsync() { yield break; }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncEnumerableVariable()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            var enumerable = GetValuesAsync();
            await foreach (var x in [|enumerable|]) { }
        }

        private static async IAsyncEnumerable<int> GetValuesAsync() { yield break; }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncForeachOfCustomEnumerable()
        {
            const string code = @"
namespace RandomNamespace
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            foreach (var y in new List<string>()) { }
            await foreach (var x in [|new AsyncEnumerable()|]) { }
        }

        private class AsyncEnumerable : IAsyncEnumerable<int> 
        {
            public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default) => null;
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncUsingVariable()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            var ad = new AsyncDisposable();
            await using([|ad|]) { }
        }
    
        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncUsing()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            await using([|new AsyncDisposable()|]) { }
        }
    
        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncUsingNoBraces()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            await using IAsyncDisposable x = [|CreateAsyncDisposable()|],
                                         y = [|new AsyncDisposable()|];
        }

        private static IAsyncDisposable CreateAsyncDisposable() => new AsyncDisposable();

        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012WarningOnAsyncUsingVariableNoBraces()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            var ad = new AsyncDisposable();
            await using var _ = [|ad|];
        }
    
        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, "AZC0012");
        }

        [Fact]
        public async Task AZC0012NoWarningOnAsyncUsingExistingConfigureAwaitFalse()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            var ad = new AsyncDisposable();
            await using(ad.ConfigureAwait(false)) { }
        }
    
        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnAsyncUsingNoBracesExistingConfigureAwaitFalse()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            var ad = new AsyncDisposable();
            await using ConfiguredAsyncDisposable x = CreateAsyncDisposable().ConfigureAwait(false), y = ad.ConfigureAwait(false);
        }

        private static IAsyncDisposable CreateAsyncDisposable() => new AsyncDisposable();

        private class AsyncDisposable : IAsyncDisposable
        {
            public ValueTask DisposeAsync() => new ValueTask();
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnUsing()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            using(new Disposable()) { }
        }
    
        private class Disposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnUsingNoBraces()
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            using var _ = new Disposable();
        }
    
        private class Disposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task AZC0012NoWarningOnCSharp7() 
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task Foo()
        {
            foreach (var x in GetValuesAsync()) { }
            using(new Disposable()) { }
        }
    
        private class Disposable : IDisposable
        {
            public void Dispose() { }
        }

        private static IEnumerable<int> GetValuesAsync() { yield break; }
    }
}";
            await Verifier.VerifyAnalyzerAsync(code, languageVersion: LanguageVersion.CSharp7);
        }

        [Fact]
        public async Task AZC0012NonCompilableCode() 
        {
            const string code = @"
namespace RandomNamespace
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MyClass
    {
        public static async Task M00() => await Task.Foo();
        public static async Task M01() => await Task.Foo().ConfigureAwait(false);
        public static async Task M02() => await Task.Delay(0).ConfigureAwait(0);
        public static async Task M03() => await Task.Foo();
        public static async Task M10() { await foreach () { } }
        public static async Task M11() { await foreach (var x in ) { } }
        public static async Task M12() { await foreach (var x in enum.ConfigureAwait(false)) { } }
        public static async Task M20() { await using var ; }
        public static async Task M21() { await using var a = ; }
        public static async Task M22() { await using(){} }
        public static async Task M23() { await using(var ){} }
        public static async Task M24() { await using(var a = ){} }
    }
}";
            await new AzureAnalyzerTest<AddConfigureAwaitAnalyzer>
            {
                CompilerDiagnostics = CompilerDiagnostics.None,
                TestCode = code,
                TestBehaviors = TestBehaviors.SkipGeneratedCodeCheck
            }.RunAsync();
        }
    }
}