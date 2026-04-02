# Quick Start: Running & Viewing CI/CD Results

## For Students: What to Expect

### Local Development (Your Machine)

Run tests locally before pushing:
```bash
# Run all tests
dotnet test

# Run with code coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run specific test
dotnet test --filter "FullyQualifiedName~Movie_Constructor_InitializesProperties"
```

### GitHub Actions (After Push)

1. **Push code to `develop` or `master`**
   ```bash
   git add .
   git commit -m "Add new feature"
   git push origin develop
   ```

2. **GitHub automatically triggers the workflow**
   - Go to **Actions** tab in your repo
   - Click the latest workflow run
   - Watch it progress through each step in real-time

3. **View Results**
   - ✅ **Test Output**: See which tests passed/failed
   - 📊 **Coverage**: Check code coverage percentage
   - 📦 **Artifacts**: Download test reports and binaries

---

## Understanding Workflow Status

### ✅ All Green (Passing)
```
✅ Checkout
✅ Setup .NET
✅ Restore dependencies
✅ Build project
✅ Run tests with code coverage
✅ Upload test results
✅ Upload code coverage
✅ Publish build artifacts
```
**Result**: PR can be merged (assuming branch protection is enabled)

### ❌ Red (Failed)
```
✅ Checkout
✅ Setup .NET
✅ Restore dependencies
❌ Build project
─ Run tests (skipped)
─ Upload test results (skipped)
─ Upload code coverage (skipped)
─ Publish build artifacts (skipped)
```
**Result**: Must fix compilation errors before tests run

### ⚠️ Yellow (Tests Failed)
```
✅ Checkout
✅ Setup .NET
✅ Restore dependencies
✅ Build project
❌ Run tests with code coverage
⚠️ Upload test results (still runs due to `if: always()`)
⚠️ Upload code coverage (still runs due to `if: always()`)
─ Publish build artifacts (skipped)
```
**Result**: Tests failed; review artifacts to understand why

---

## Finding Artifacts

### Step 1: Navigate to Workflow Run
- **GitHub Repo** → **Actions** tab
- Click the failed/passing run
- Scroll to **Artifacts** section at bottom

### Step 2: Download Your Artifacts
Three types available:

#### 📋 `test-results`
- Contains `test-results.trx` file
- Open in Visual Studio to see detailed test failures
- Shows stack traces, exception messages, timing info

#### 📊 `code-coverage-reports`
- Contains `coverage.opencover.xml` (detailed)
- Contains `coverage.cobertura.xml` (summary)
- Use tools like ReportGenerator to visualize

#### 📦 `build-artifacts`
- Compiled DLLs and EXE files
- Use for local testing or deployment
- Matches what would be deployed to production

---

## Interpreting Test Reports

### TRX File (Test Results XML)

Download `test-results.trx` and open in Visual Studio:

1. **Visual Studio** → **Test** → **Open** → select `.trx` file
2. See test timeline, pass/fail breakdown
3. Click failed test to see error details

### Coverage Reports

For detailed coverage analysis:

```bash
# Generate HTML report from OpenCover XML
dotnet tool install -g ReportGenerator

reportgenerator -reports:"coverage.opencover.xml" -targetdir:"coveragereport"
```

Open `coveragereport/index.html` to see:
- Line-by-line coverage
- Branch coverage stats
- Missing coverage highlights

---

## The Development Workflow

### Scenario: You Have a Failing Test

**Your workflow:**

1. **Make changes locally**
   ```bash
   # Edit Movie.cs
   # Run tests
   dotnet test  # ❌ Fails
   ```

2. **Review test output**
   ```
   Expected: Inception
   Actual: null
   ```

3. **Fix your code**
   ```bash
   # Fix Movie.cs
   dotnet test  # ✅ Passes
   ```

4. **Push to GitHub**
   ```bash
   git add .
   git commit -m "Fix Movie initialization"
   git push origin develop
   ```

5. **Watch GitHub Actions**
   - Workflow runs automatically
   - All tests pass
   - Artifacts uploaded

6. **Create Pull Request**
   - Go to GitHub → **Pull requests** → **New**
   - Fill in description
   - GitHub shows: "✅ All checks passed"
   - Can now merge to `master`

---

## Troubleshooting Failed Workflows

| Error | Check |
|-------|-------|
| `dotnet: command not found` | Workflow SDK setup step failed; check .NET version in `main.yml` |
| `Project file not found` | Check project paths in `.csproj` files and workflow `run:` steps |
| `0 tests found` | Test project not referenced in `.csproj`; check `<ProjectReference>` |
| `No artifacts` | Check `path:` glob patterns; artifacts only upload if tests run successfully |
| `Coverage files missing` | Ensure Coverlet is installed: `dotnet list package \| grep coverlet` |

---

## Extending for Your Team

### Add More Tests

Create new test methods in `MovieTests.cs`:

```csharp
[Fact]
public void Movie_SetGenre_UpdatesGenreProperty()
{
    var movie = new Movie { Genre = "Drama" };
    movie.Genre = "Action";
    Assert.Equal("Action", movie.Genre);
}
```

Push and GitHub Actions automatically runs it!

### Add Test Coverage Threshold

In workflow, add after test step:

```yaml
- name: Check coverage threshold
  run: |
    if (-not (Select-String -Path coverage.opencover.xml -Pattern 'lineCoverage="([89]\d|100)"')) {
      Write-Error "Coverage below 80%"
      exit 1
    }
```

---

## Key Takeaways

- ✅ **Automation saves time**: Push → GitHub runs everything
- ✅ **Consistency**: Everyone runs the same pipeline
- ✅ **Safety net**: Bad code won't merge to `master`
- ✅ **Visibility**: Artifacts show exactly what happened
- ✅ **Scalability**: Add more tests/checks without changing process
