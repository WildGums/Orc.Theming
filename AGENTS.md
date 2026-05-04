# Orc.Theming

Orc.Theming is a theming library for WildGums WPF applications. It provides a thin convenience layer on top of the [ControlzEx](https://github.com/ControlzEx/ControlzEx) theming system, enabling consistent accent colors, base color schemes, font sizes, and resource dictionary management across all WildGums libraries.

The library consists of a single project:

- `Orc.Theming` — WPF theming library targeting `net8.0-windows`, `net9.0-windows`, and `net10.0-windows`.

---

## Critical Rules (Read First)

These rules are **non-negotiable**. Violating them causes broken builds, crashes, or downstream breakage.

### 1. Never Edit Generated Files

Files matching `*.generated.cs` and `*.generated.xaml` (e.g. `Themes\Generic.generated.xaml`) are auto-generated.

- **NEVER** manually edit these files

### 2. ABI / API Stability

This project maintains stable ABI / API. Breaking changes break downstream apps.

| Allowed | Never |
|---------|-------|
| Add new overloads | Modify existing signatures |
| Add new methods | Remove public APIs |
| Add new classes | Change return types |

### 3. Tests Are Mandatory

**Building alone is NOT sufficient.** Run tests before claiming completion (see [Commands](#commands)).

### 4. Branch Protection (COMPLIANCE REQUIRED)

**Direct commits to protected branches are a policy violation.**

| Repository | Protected Branches |
|------------|-------------------|
| Orc.Theming | `master` |
| Orc.Theming | `develop` |

**Required workflow:**

1. **Create a feature branch FIRST** — Use naming convention: `feature/issue-NNNN-description`
2. **Make all commits on the feature branch** — Never commit directly to protected branches
3. **Submit a Pull Request** — Changes must be reviewed by a human before merging

```bash
# CORRECT — Always create a feature branch first
git checkout -b feature/issue-1234-fix-description

# NEVER DO THIS — Policy violation
git checkout develop && git commit  # FORBIDDEN

# NEVER DO THIS — Policy violation
git checkout master && git commit  # FORBIDDEN
```

---

## Commands

Single source of truth for all commands:

| Task | Command |
|------|---------|
| **Build** | `dotnet cake --target=build` |
| **Test** | `dotnet cake --target=test` |
| **Build and test** | `dotnet cake --target=buildandtest` |

---

## Architecture & Directories

### Layer Overview

```
Orc.Theming => WPF theming library (net8.0-windows / net9.0-windows / net10.0-windows)
```

### Key Namespaces

| Namespace | Purpose |
|-----------|---------|
| `Orc.Theming` | Core types, services, and managers |
| `Orc.Theming.Coloring` | Color generation utilities |
| `Orc.Theming.Controls` | Themed WPF controls |
| `Orc.Theming.Converters` | WPF value converters for theming |
| `Orc.Theming.Extensions` | Extension methods |
| `Orc.Theming.Helpers` | Internal helper types |

### Key Services

| Service Interface | Default Implementation | Purpose |
|------------------|----------------------|---------|
| `IAccentColorService` | `AccentColorService` | Provides the current accent color |
| `IBaseColorSchemeService` | `BaseColorSchemeService` | Provides the active base color scheme (e.g. Light / Dark) |
| `IFontSizeService` | `FontSizeService` | Provides application font size settings |
| `IResourceDictionaryService` | `ResourceDictionaryService` | Manages merged resource dictionaries |
| `IThemeService` | `ThemeService` | Builds `ThemeInfo` and controls style forwarders |

### Directory Guide

| Directory | Editable? | Notes |
|-----------|-----------|-------|
| `src/Orc.Theming/` | Yes | Main library source |
| `src/Orc.Theming/Themes/` | Yes (hand-written XAML only) | WPF resource dictionaries |
| `src/Orc.Theming/Themes/*.generated.xaml` | **No** | Auto-generated — do not edit |
| `src/Orc.Theming.Tests/` | Yes | NUnit test project |
| `src/Orc.Theming.Example/` | Yes | WPF example application |
| `deployment/` | No | Build / deployment scripts |

---

## Writing Code

### Coding Conventions

- Target `net8.0-windows`, `net9.0-windows`, and `net10.0-windows` — no platform-agnostic APIs
- Use **Catel.MVVM** patterns for ViewModels (IoC, `ViewModelBase`, etc.)
- Register all services via `OrcThemingModule.AddOrcTheming(IServiceCollection)` — never use `new` for services
- Use `ArgumentNullException.ThrowIfNull` for null guards
- Nullable reference types are **enabled** — annotate accordingly
- Fody weavers (`Catel.Fody`, `MethodTimer.Fody`, `Obsolete.Fody`) run at compile time; do not add redundant manual implementations

### Anti-Patterns (Never Do This)

| Anti-Pattern | Why |
|-------------|-----|
| Modifying method signatures of public APIs | ABI breaking |
| Manual edits to `*.generated.cs` or `*.generated.xaml` | Overwritten on regenerate |
| Using default parameters in public APIs | ABI breaking |
| Registering services with `new` instead of the DI container | Breaks IoC and testability |
| **Skipping failing tests** | **Unacceptable — tests must pass** |

---

## Testing & Debugging

### Running Tests

```bash
dotnet cake --target=test
```

### Tests MUST Pass

> **NON-NEGOTIABLE:** Tests must PASS before claiming completion.
>
> - Do NOT skip failing tests
> - Do NOT claim completion if tests fail
> - Do NOT use `SkipException` to work around failures

### Writing Tests

1. Use **NUnit** to write tests (the project already uses NUnit 4.x)
2. Place tests in `src/Orc.Theming.Tests/`
3. Use Pascal / Snake case for test method names: `Feature_Does_Work`
4. Public API surface changes must be reflected in `PublicApiFacts.Orc_Theming_HasNoBreakingChanges_Async.verified.txt`

```csharp
[Test]
public void Feature_Does_Work()
{
    var result = 47 - 5;

    Assert.That(result, Is.EqualTo(42));
}
```

### Public API Approval Tests

The project uses **PublicApiGenerator** and **Verify.NUnit** to detect accidental breaking changes. If you intentionally change the public API:

1. Delete `PublicApiFacts.Orc_Theming_HasNoBreakingChanges_Async.verified.txt`
2. Run the tests once — the `.received.txt` snapshot is regenerated
3. Review the diff carefully and rename `.received.txt` → `.verified.txt`

### Debugging Methodology

1. **Establish baseline** — What is the known-good state?
2. **One change at a time** — Verify each change before proceeding
3. **Track changes in a table** — Log what you changed and the result
4. **Platform differences are signals** — If X works and Y fails, the difference IS the answer
5. **Revert if worse** — Don't pile fixes on top of failures

---

## Available Skills

The repository ships with reusable AI agent skills in `.agents/skills/`:

| Skill | Description |
|-------|-------------|
| `api-docs` | Write and review XML API documentation (`<summary>`, `<param>`, `<returns>`, etc.) |
| `docs-writer` | Create and update conceptual DocFX documentation |

---

## Further Reading

| Topic | Document |
|-------|----------|
| Contributing guidelines | `CONTRIBUTING.md` |
| PR template | `.github/PULL_REQUEST_TEMPLATE.md` |
| Documentation portal | https://opensource.wildgums.com |
