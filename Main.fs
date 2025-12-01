module UnitTests
open Expecto
open System.IO
open System.Diagnostics
open System.Management

let getParentPid (pid:int) =
    use searcher =
        new ManagementObjectSearcher(
            $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {pid}"
        )
    searcher.Get()
    |> Seq.cast<ManagementObject>
    |> Seq.tryHead
    |> Option.map (fun mo -> int (mo.["ParentProcessId"] :?> uint32))

let getCommandLine (pid:int) =
    use searcher =
        new ManagementObjectSearcher(
            $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {pid}"
        )
    searcher.Get()
    |> Seq.cast<ManagementObject>
    |> Seq.tryHead
    |> Option.bind (fun mo ->
        match mo.["CommandLine"] with
        | null -> None
        | v -> Some (v :?> string))

let isStartedByDotnetWatch () =
    let pid = Process.GetCurrentProcess().Id

    match getParentPid pid |> Option.bind getParentPid with
    | None -> false
    | Some grandParentPid ->
        match getCommandLine grandParentPid with
        | None -> false
        | Some cmd ->
            let c = cmd.ToLowerInvariant()
            c.Contains("dotnet watch") ||
            c.Contains("dotnet-watch") ||
            c.Contains("microsoft.dotnet.watcher")

[<EntryPoint>]
let main argv =
    let mutable exitCode = 0

    let w = new FileSystemWatcher("c:/users/renee/source/repos", "*.txt")
    w.EnableRaisingEvents <- true
    w.IncludeSubdirectories <- false
    w.NotifyFilter <- NotifyFilters.LastWrite
    w.Changed.Add(fun evt ->
        exitCode <- Tests.runTestsInAssemblyWithCLIArgs [] argv
    )

    exitCode <- runTestsInAssemblyWithCLIArgs [] argv
    if isStartedByDotnetWatch() then
        while true do
            System.Threading.Thread.Sleep 1000
    exitCode



