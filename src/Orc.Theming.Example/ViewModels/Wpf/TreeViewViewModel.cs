namespace Orc.Theming.Example.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel.MVVM;

    public class TreeNode
    {
        public string Name { get; set; }
#pragma warning disable CA2227 // Collection properties should be read only
        public List<TreeNode> ChildNodes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }

    public class TreeViewViewModel : ViewModelBase
    {
        public TreeViewViewModel()
        {
            TreeNodes = new List<TreeNode>
            {
                new TreeNode
                {
                    Name = "Level_0(1)",
                    ChildNodes = new List<TreeNode>
                    {
                        new TreeNode
                        {
                            Name = "Level_1(1)"
                        },

                        new TreeNode
                        {
                            Name = "Level_1(2)"
                        },
                    }
                },
                new TreeNode
                {
                    Name = "Level_0(2)",
                    ChildNodes = new List<TreeNode>
                    {
                        new TreeNode
                        {
                            Name = "Level_1(1)"
                        },

                        new TreeNode
                        {
                            Name = "Level_1(2)"
                        },
                    }
                },
                new TreeNode
                {
                    Name = "Level_0(3)",
                    ChildNodes = new List<TreeNode>
                    {
                        new TreeNode
                        {
                            Name = "Level_1(1)"
                        },

                        new TreeNode
                        {
                            Name = "Level_1(2)"
                        },
                    }
                }
            };
        }

        public override string Title => "View model title";

#pragma warning disable CA2227 // Collection properties should be read only
        public IList<TreeNode> TreeNodes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
