using Newtonsoft.Json;

namespace JobPlanner.GitJson
{
    public class InfoGit
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("owner")]
        public OwnerGit Owner { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fork")]
        public bool Fork { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("forks_url")]
        public string ForksUrl { get; set; }

        [JsonProperty("keys_url")]
        public string KeysUrl { get; set; }

        [JsonProperty("collaborators_url")]
        public string CollaboratorsUrl { get; set; }

        [JsonProperty("teams_url")]
        public string Teams_url { get; set; }

        [JsonProperty("hooks_url")]
        public string Hooks_url { get; set; }

        [JsonProperty("issue_events_url")]
        public string IssueEvents_url { get; set; }

        [JsonProperty("events_url")]
        public string Events_url { get; set; }

        [JsonProperty("assignees_url")]
        public string Assignees_url { get; set; }

        [JsonProperty("branches_url")]
        public string Branches_url { get; set; }

        [JsonProperty("tags_url")]
        public string Tags_url { get; set; }

        [JsonProperty("blobs_url")]
        public string Blobs_url { get; set; }

        [JsonProperty("git_tags_url")]
        public string GitTags_url { get; set; }

        [JsonProperty("git_refs_url")]
        public string GitRefs_url { get; set; }

        [JsonProperty("trees_url")]
        public string Trees_url { get; set; }

        [JsonProperty("statuses_url")]
        public string Statuses_url { get; set; }

        [JsonProperty("languages_url")]
        public string Languages_url { get; set; }

        [JsonProperty("stargazers_url")]
        public string Stargazers_url { get; set; }

        [JsonProperty("contributors_url")]
        public string Contributors_url { get; set; }

        [JsonProperty("subscribers_url")]
        public string Subscribers_url { get; set; }

        [JsonProperty("subscription_url")]
        public string Subscription_url { get; set; }

        [JsonProperty("commits_url")]
        public string Commits_url { get; set; }

        [JsonProperty("git_commits_url")]
        public string GitCommits_url { get; set; }

        [JsonProperty("comments_url")]
        public string Comments_url { get; set; }

        [JsonProperty("issue_comment_url")]
        public string IssueComment_url { get; set; }

        [JsonProperty("contents_url")]
        public string Contents_url { get; set; }

        [JsonProperty("compare_url")]
        public string Compare_url { get; set; }

        [JsonProperty("merges_url")]
        public string Merges_url { get; set; }

        [JsonProperty("archive_url")]
        public string Archive_url { get; set; }

        [JsonProperty("downloads_url")]
        public string Downloads_url { get; set; }

        [JsonProperty("issues_url")]
        public string Issues_url { get; set; }

        [JsonProperty("pulls_url")]
        public string Pulls_url { get; set; }

        [JsonProperty("milestones_url")]
        public string Milestones_url { get; set; }

        [JsonProperty("notifications_url")]
        public string Notifications_url { get; set; }

        [JsonProperty("labels_url")]
        public string Labels_url { get; set; }

        [JsonProperty("releases_url")]
        public string Releases_url { get; set; }

        [JsonProperty("deployments_url")]
        public string Deployments_url { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("pushed_at")]
        public string PushedAt { get; set; }

        [JsonProperty("git_url")]
        public string Git_url { get; set; }

        [JsonProperty("ssh_url")]
        public string Ssh_url { get; set; }

        [JsonProperty("clone_url")]
        public string Clone_url { get; set; }

        [JsonProperty("svn_url")]
        public string Svn_url { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("has_issues")]
        public bool HasIssues { get; set; }

        [JsonProperty("has_projects")]
        public bool HasProjects { get; set; }

        [JsonProperty("has_downloads")]
        public bool HasDownloads { get; set; }

        [JsonProperty("has_wiki")]
        public bool HasWiki { get; set; }

        [JsonProperty("has_pages")]
        public bool HasPages { get; set; }

        [JsonProperty("forks_count")]
        public int ForksCount { get; set; }

        [JsonProperty("mirror_url")]
        public string Mirror_url { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonProperty("license")]
        public LicenseGit License { get; set; }

        [JsonProperty("forks")]
        public int Forks { get; set; }

        [JsonProperty("open_issues")]
        public int OpenIssues { get; set; }

        [JsonProperty("watchers")]
        public int Watchers { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("permissions")]
        public PermissionsGit Permissions { get; set; }
    }
}
