"use strict";(self.webpackChunkdocusaurus=self.webpackChunkdocusaurus||[]).push([[331],{3369:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>a,contentTitle:()=>l,default:()=>c,frontMatter:()=>s,metadata:()=>r,toc:()=>u});var o=n(4848),i=n(8453);const s={title:"Module History"},l="Module History",r={id:"how-to/storing-and-retrieving-results",title:"Module History",description:"What is it?",source:"@site/docs/how-to/storing-and-retrieving-results.md",sourceDirName:"how-to",slug:"/how-to/storing-and-retrieving-results",permalink:"/ModularPipelines/docs/how-to/storing-and-retrieving-results",draft:!1,unlisted:!1,tags:[],version:"current",frontMatter:{title:"Module History"},sidebar:"tutorialSidebar",previous:{title:"Skipping Modules",permalink:"/ModularPipelines/docs/how-to/skipping"},next:{title:"Sub-Modules",permalink:"/ModularPipelines/docs/how-to/sub-modules"}},a={},u=[{value:"What is it?",id:"what-is-it",level:2},{value:"Example Repository Class using Azure Blobs",id:"example-repository-class-using-azure-blobs",level:2}];function d(e){const t={code:"code",h1:"h1",h2:"h2",p:"p",pre:"pre",...(0,i.R)(),...e.components};return(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)(t.h1,{id:"module-history",children:"Module History"}),"\n",(0,o.jsx)(t.h2,{id:"what-is-it",children:"What is it?"}),"\n",(0,o.jsx)(t.p,{children:"If a repository has been set up, then when a module finishes without throwing an exception, it will attempt to save its result in the repository."}),"\n",(0,o.jsx)(t.p,{children:"This can be used later on if you want to re-run the pipeline but skip certain categories or modules."}),"\n",(0,o.jsx)(t.p,{children:"If a module is skipped, but it has a result available from a previous run, then it'll be reconstructed from the historical result, and it'll act as if it was successfully run."}),"\n",(0,o.jsxs)(t.p,{children:["It's recommended to store and retrieve results using the Git commit SHA, as then you'll only be using the results from that previous commit's run. And other commits remain unaffected. You have access to a ",(0,o.jsx)(t.code,{children:"context"})," object that gives you access to things like the git information."]}),"\n",(0,o.jsx)(t.h2,{id:"example-repository-class-using-azure-blobs",children:"Example Repository Class using Azure Blobs"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"await PipelineHostBuilder.Create()\n    .AddModule<Module1>()\n    .AddModule<Module2>()\n    .AddModule<Module3>()\n    .AddResultsRepository<MyModuleRepository>()\n    .ExecutePipelineAsync();\n\n"})}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"public class MyModuleRepository : IModuleResultRepository\n{\n    private readonly BlobContainerClient _blobContainerClient;\n\n    public MyModuleRepository(BlobContainerClient blobContainerClient)\n    {\n        _blobContainerClient = blobContainerClient;\n    }\n    \n    public async Task SaveResultAsync<T>(ModuleBase module, ModuleResult<T> moduleResult, IPipelineHookContext pipelineContext)\n    {\n        var commit = pipelineContext.Git().Information.LastCommitSha;\n        await _blobContainerClient.UploadBlobAsync(module.GetType().FullName + commit, new BinaryData(JsonSerializer.Serialize(moduleResult)));\n    }\n\n    public async Task<ModuleResult<T>?> GetResultAsync<T>(ModuleBase module, IPipelineHookContext pipelineContext)\n    {\n        var commit = pipelineContext.Git().Information.LastCommitSha;\n        \n        var blobContent = await _blobContainerClient.GetBlobClient(module.GetType().FullName + commit).DownloadContentAsync();\n\n        return JsonSerializer.Deserialize<ModuleResult<T>>(blobContent.Value.Content.ToString());\n    }\n}\n"})})]})}function c(e={}){const{wrapper:t}={...(0,i.R)(),...e.components};return t?(0,o.jsx)(t,{...e,children:(0,o.jsx)(d,{...e})}):d(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>l,x:()=>r});var o=n(6540);const i={},s=o.createContext(i);function l(e){const t=o.useContext(s);return o.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function r(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(i):e.components||i:l(e.components),o.createElement(s.Provider,{value:t},e.children)}}}]);