# CI/CD Pipeline & Branch Protection Guide

## Overview

This guide demonstrates GitHub Actions CI/CD fundamentals using a real-world movie database application. Students will learn:

- **Automated Testing**: Unit tests run on every push
- **Code Coverage**: Measuring test coverage with Coverlet
- **Artifact Management**: Publishing test results & binaries
- **Branch Protection**: Enforcing quality gates before merging
- **Pipeline as Code**: Version-controlled workflow definitions

---

## What's in the Workflow?

Located at `.github/workflows/main.yml`, the pipeline runs on every push to `master` and `develop` branches, plus all pull requests.

### Pipeline Stages

1. **Checkout** - Clone repository code
2. **Setup .NET** - Install .NET 10 SDK
3. **Restore** - Download NuGet dependencies
4. **Build** - Compile in Release configuration
5. **Test + Coverage** - Run xUnit tests with Coverlet code coverage
6. **Upload Test Results** - Publish TRX files as artifacts
7. **Upload Coverage Reports** - Publish OpenCover & Cobertura XML files
8. **Publish Artifacts** - Publish compiled DLL/EXE binaries

### Key Features

#### Strict Enforcement
If ANY step fails, the pipeline **stops immediately**. This means:
- If tests fail → pipeline fails → no artifacts published
- If build fails → tests never run
- This prevents deploying broken code

#### Code Coverage with Coverlet
```bash
dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover,cobertura
```
- **OpenCover format**: Detailed XML coverage reports
- **Cobertura format**: Standard coverage format for tools
- Integrated directly into xUnit test execution

#### Artifact Retention
All artifacts are retained for **30 days**, allowing:
- Review of failed test runs
- Comparison of coverage trends
- Download of specific build versions

---

## Understanding the Unit Tests

### Simple Movie Model Test

Located in `CLDV POE 1 Example Movie Database.Tests/MovieTests.cs`:

```csharp
[Fact]
public void Movie_Constructor_InitializesProperties()
{
    // Arrange: Set up test data
    var movie = new Movie { Title = "Inception", Genre = "Sci-Fi" };
    
    // Act: Implicitly done in Arrange
    
    // Assert: Verify behavior
    Assert.Equal("Inception", movie.Title);
}
```

### Test Framework: xUnit

Why xUnit? 
- Industry standard (used by .NET Core team)
- Clean, readable syntax
- Excellent Visual Studio integration
- Great for teaching fundamentals

### Running Tests Locally

```bash
# Run all tests
dotnet test

# Run with code coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run specific test class
dotnet test --filter "FullyQualifiedName~MovieTests"
```

---

## Branch Protection Setup (GitHub)

### Why Branch Protection?

Ensures code quality by preventing direct pushes to `master` and requiring:
- ✅ Passing CI/CD checks
- ✅ Pull request review
- ✅ Up-to-date branches

### Configure in GitHub

1. **Go to Settings → Branches**
2. **Add rule** for `master` branch:
   - ✅ Require status checks to pass before merging
   - ✅ Require branches to be up to date
   - ✅ (Optional) Require pull request reviews
3. **Select required status checks**:
   - `build-and-test` (the job name from workflow)
4. **Click Save**

### What Happens Now

```
Developer pushes code
    ↓
GitHub Actions runs workflow (build-and-test job)
    ↓
Tests pass? → Can merge to master
Tests fail? → PR blocked, developer must fix
```

---

## Workflow YAML Breakdown

### Trigger Configuration
```yaml
on:
  push:
    branches: [ master, develop ]
  pull_request:
    branches: [ master, develop ]
```
- Runs on push to `master`/`develop`
- Runs on ALL pull requests targeting these branches
- No manual trigger needed

### Test Step with Coverage
```yaml
- name: Run tests with code coverage
  run: |
    dotnet test --configuration Release --no-build \
      --logger "trx;LogFileName=test-results.trx" \
      --collect:"XPlat Code Coverage" \
      -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover,cobertura
```

**Flags explained:**
- `--configuration Release` - Build optimized binaries (matches build step)
- `--no-build` - Skip rebuild; use artifacts from build step
- `--logger "trx;..."` - Output test results in TRX format for artifacts
- `--collect:"XPlat Code Coverage"` - Enable Coverlet coverage collection
- `Format=opencover,cobertura` - Output both XML formats

### Artifact Upload Pattern
```yaml
- name: Upload test results
  if: always()
  uses: actions/upload-artifact@v4
  with:
    name: test-results
    path: '**/test-results.trx'
    retention-days: 30
```

**Key points:**
- `if: always()` - Upload even if tests fail (for debugging)
- `path: '**/test-results.trx'` - Glob pattern finds files in any subdirectory
- `retention-days: 30` - Auto-delete after 30 days

---

## Common Workflow Scenarios

### Scenario 1: Developer Pushes Failing Tests

```
Developer: "Let me push this feature..."
  ↓
GitHub: Running tests...
  ↓
Pipeline: ❌ Test failed
  ↓
Developer: "I can't merge! Let me check the artifacts..."
  ↓
Developer downloads test-results.trx, reviews failure
  ↓
Developer fixes bug, pushes again
  ↓
Pipeline: ✅ All tests pass
  ↓
PR can now be merged to master
```

### Scenario 2: Pull Request Code Review

```
Developer opens PR to master
  ↓
GitHub: "Running required checks..."
  ↓
Pipeline starts automatically (no action needed)
  ↓
Pipeline completes (passes or fails)
  ↓
Code reviewer sees: ✅ All checks passed
  ↓
Reviewer approves PR
  ↓
PR is merged to master
```

---

## Extending the Pipeline

### Add More Test Projects
Just create a new `*.Tests.csproj` file; `dotnet test` automatically discovers and runs all test projects.

### Add Code Quality Checks
```yaml
- name: Run StyleCop analyzers
  run: dotnet build /p:TreatWarningsAsErrors=true
```

### Deploy to Azure
```yaml
- name: Deploy to Azure App Service
  uses: azure/webapps-deploy@v3
  with:
    app-name: 'my-app'
    package: './bin/Release/net10.0'
```

### Send Notifications
```yaml
- name: Notify Slack on failure
  if: failure()
  run: |
    curl -X POST ${{ secrets.SLACK_WEBHOOK }} ...
```

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Tests don't run in workflow | Check that test project references the main project |
| Coverage files not found | Verify Coverlet package installed: `dotnet list package \| grep coverlet` |
| Artifacts not uploading | Check `path:` glob pattern with `dotnet build --list-sdks` to understand output structure |
| Branch protection not working | Ensure job name in workflow exactly matches required status checks in GitHub settings |

---

## Learning Outcomes

After this project, students should understand:

- ✅ CI/CD workflow definition and orchestration
- ✅ Automated test execution and enforcement
- ✅ Code coverage measurement tools (Coverlet)
- ✅ Artifact retention and management
- ✅ Branch protection and quality gates
- ✅ Pull request and code review integration
- ✅ Troubleshooting failed builds
- ✅ Extending pipelines with custom steps

---

## References

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Coverlet Code Coverage](https://github.com/coverlet-coverage/coverlet)
- [xUnit Testing Framework](https://xunit.net/)
- [Branch Protection Rules](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches)
- [Artifacts Upload](https://github.com/actions/upload-artifact)
