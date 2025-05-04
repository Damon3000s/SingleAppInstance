## v1.2.6-pre.2 (prerelease)

Incremental prerelease update.
## v1.2.6-pre.1 (prerelease)

Changes since v1.2.5:

- Sync scripts\PSBuild.psm1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.2.5 (patch)

Changes since v1.2.4:

- Remove .markdownlint.json configuration file, update DESCRIPTION.md for clarity, and change project SDK references in .csproj files to ktsu.Sdk.Lib and ktsu.Sdk.Test version 1.8.0. ([@matt-edmondson](https://github.com/matt-edmondson))
- Remove Directory.Build.props, Directory.Build.targets, and several PowerShell scripts for metadata and version management. Update SingleAppInstance and its tests to use 'var' for variable declarations and add copyright information. ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.2.5-pre.3 (prerelease)

Changes since v1.2.5-pre.2:

- Bump ktsu.AppDataStorage from 1.11.0 to 1.15.0 in the ktsu group ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.2.5-pre.2 (prerelease)

Changes since v1.2.5-pre.1:

- Bump ktsu.AppDataStorage from 1.7.2 to 1.11.0 in the ktsu group ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.2.5-pre.1 (prerelease)

Incremental prerelease update.
## v1.2.4 (patch)

Changes since v1.2.3:

- Update README to match standard template format ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.2.3 (patch)

Changes since v1.2.2:

- Add tests for SingleAppInstance behavior with no other instance running and custom launch logic ([@matt-edmondson](https://github.com/matt-edmondson))
- Enhance SingleAppInstance to store and verify process information in JSON format ([@matt-edmondson](https://github.com/matt-edmondson))
- Enhance memory.jsonl to include advanced process detection and compatibility details for SingleAppInstance class ([@matt-edmondson](https://github.com/matt-edmondson))
- Refine README.md for clarity and usage instructions of SingleAppInstance ([@matt-edmondson](https://github.com/matt-edmondson))
- Update language-specific guidelines to clarify test execution commands and improve resource management ([@matt-edmondson](https://github.com/matt-edmondson))
- Refine README.md to enhance feature descriptions and clarify technical implementation details for SingleAppInstance ([@matt-edmondson](https://github.com/matt-edmondson))
- Set max cpus for tests to 1 ([@matt-edmondson](https://github.com/matt-edmondson))
- Add memory.jsonl file with project and class details ([@matt-edmondson](https://github.com/matt-edmondson))
- Update test command to run in a single process ([@matt-edmondson](https://github.com/matt-edmondson))
- Enhance memory.jsonl to include additional details for SingleAppInstance tests and project structure ([@matt-edmondson](https://github.com/matt-edmondson))
- Refactor SingleAppInstanceTests to simplify file deletion logic and remove unnecessary try-finally blocks, ensuring clearer test flow and improved readability. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update Copilot instructions for .NET project build and testing guidelines ([@matt-edmondson](https://github.com/matt-edmondson))
- Refactor SingleAppInstanceTests for better isolation ([@matt-edmondson](https://github.com/matt-edmondson))
- Organize and enhance Copilot documentation with detailed guidelines on memory usage, coding standards, and project management practices. ([@matt-edmondson](https://github.com/matt-edmondson))
- Refine workflow guidelines for clarity on specialized tool usage and documentation checks ([@matt-edmondson](https://github.com/matt-edmondson))
- Add SingleAppInstance.Test project to solution and configure build settings ([@matt-edmondson](https://github.com/matt-edmondson))
- Refactor SingleAppInstanceTests to improve clarity and structure of test methods, ensuring accurate PID file handling and process information validation. ([@matt-edmondson](https://github.com/matt-edmondson))
- Update language-specific guidelines to clarify tool usage and fallback options ([@matt-edmondson](https://github.com/matt-edmondson))
- Add workflow and process guidelines to Copilot instructions ([@matt-edmondson](https://github.com/matt-edmondson))
- Comment out MaxCpuCount setting in .runsettings to disable process-level parallelization ([@matt-edmondson](https://github.com/matt-edmondson))
- Clarify command line usage by providing an example for non-interactive mode with `git` commands ([@matt-edmondson](https://github.com/matt-edmondson))
- Enhance SingleAppInstanceTests by adding assertions to verify PID file handling and initial state checks ([@matt-edmondson](https://github.com/matt-edmondson))
- Add guidelines for using command line in non-interactive mode and directory context ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.2.2 (patch)

Changes since v1.2.1:

- Add comprehensive Copilot instructions and memory management guidelines ([@matt-edmondson](https://github.com/matt-edmondson))
- Add markdownlint configuration file for linting rules ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.2.2-pre.3 (prerelease)

Changes since v1.2.2-pre.2:

- Bump ktsu.AppDataStorage from 1.7.1 to 1.7.2 in the ktsu group ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.2.2-pre.2 (prerelease)

Changes since v1.2.2-pre.1:

- Sync .editorconfig ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.2.2-pre.1 (prerelease)

Incremental prerelease update.
## v1.2.1 (patch)

Changes since v1.2.0:

- Update packages ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.2.0 (minor)

Changes since v1.1.0:

- Add LICENSE template ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.1.1-pre.2 (prerelease)

Changes since v1.1.1-pre.1:

- Sync scripts\make-changelog.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\make-version.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.1.1-pre.1 (prerelease)

Changes since v1.1.0:

- Sync .editorconfig ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\make-changelog.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync .gitignore ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync Directory.Build.targets ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\make-version.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.1.0 (minor)

Changes since v1.0.0-pre.19:

- Apply new editorconfig ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.0.0-pre.19 (prerelease)

Changes since v1.0.0-pre.18:

- Bump ktsu.AppDataStorage from 1.4.7 to 1.5.0 in the ktsu group ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.0.0-pre.18 (prerelease)

Changes since v1.0.0-pre.17:

- Sync scripts\make-version.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\make-changelog.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0-pre.17 (prerelease)

Changes since v1.0.0-pre.16:

- Sync .github\workflows\dotnet.yml ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0-pre.16 (prerelease)

Changes since v1.0.0-pre.15:

- Sync scripts\make-changelog.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
- Sync scripts\make-version.ps1 ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0-pre.15 (prerelease)

Changes since v1.0.0-pre.14:

- Sync .github\workflows\dotnet.yml ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0-pre.14 (prerelease)

Changes since v1.0.0-pre.13:

- Review Feedback ([@Damon3000s](https://github.com/Damon3000s))
- Revert unintended changes ([@Damon3000s](https://github.com/Damon3000s))
- Create the directory the Pid will be stored in ([@Damon3000s](https://github.com/Damon3000s))
## v1.0.0-pre.13 (prerelease)

Changes since v1.0.0-pre.12:

- Sync .github\workflows\dotnet.yml ([@ktsu[bot]](https://github.com/ktsu[bot]))
## v1.0.0-pre.12 (prerelease)

Changes since v1.0.0-pre.11:
## v1.0.0-pre.11 (prerelease)

Changes since v1.0.0-pre.10:
## v1.0.0-pre.10 (prerelease)

Changes since v1.0.0-pre.9:

- Catch DirectoryNotFoundException ([@Damon3000s](https://github.com/Damon3000s))
## v1.0.0-pre.9 (prerelease)

Changes since v1.0.0-pre.8:

- Bump MSTest from 3.7.2 to 3.7.3 ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.0.0-pre.8 (prerelease)

Changes since v1.0.0-pre.7:
## v1.0.0-pre.7 (prerelease)

Changes since v1.0.0-pre.6:
## v1.0.0-pre.6 (prerelease)

Changes since v1.0.0-pre.5:

- Bump MSTest from 3.7.1 to 3.7.2 ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.0.0-pre.5 (prerelease)

Changes since v1.0.0-pre.4:

- Bump coverlet.collector from 6.0.3 to 6.0.4 ([@dependabot[bot]](https://github.com/dependabot[bot]))
## v1.0.0-pre.4 (prerelease)

Changes since v1.0.0-pre.3:
## v1.0.0-pre.3 (prerelease)

Changes since v1.0.0-pre.2:
## v1.0.0-pre.2 (prerelease)

Changes since v1.0.0-pre.1:

- Remove ktsu.ScopedAction package from project ([@matt-edmondson](https://github.com/matt-edmondson))
## v1.0.0-pre.1 (prerelease)

Incremental prerelease update.
## v0.0.1-pre.1 (prerelease)

Incremental prerelease update.
