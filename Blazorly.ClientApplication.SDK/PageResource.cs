using Blazorly.ClientApplication.SDK.Interfaces;
using NanoidDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public abstract class PageResource : BaseResource
	{
		public PageResource(Type entityType) 
		{
			EntityType = entityType;
			UI = new UI();
		}

		public Type EntityType { get; set; }

		public IEntityContext EntityContext { get; set; }

		public UI UI { get; set; }

        public List<PageAction> Actions { get; set; } = new List<PageAction>();

        public List<PageBlock> Blocks { get; set; } = new List<PageBlock>();

		public void Add(PageBlock block)
		{
			if(Blocks.Where(x=>x.Name.ToUpperInvariant() == block.Name.ToUpperInvariant()).Any())
			{
				throw new Exception($"Block name must be unique: {block.Name}");
			}

            Blocks.Add(block);
        }

        public void Add(PageAction action)
        {
            if (Actions.Where(x => x.Name.ToUpperInvariant() == action.Name.ToUpperInvariant()).Any())
            {
                throw new Exception($"Action name must be unique: {action.Name}");
            }

            Actions.Add(action);
        }

		public PageBlock this[string name]
		{
			get
			{
				var block = Blocks.Where(x => x.Name.ToUpperInvariant() == name.ToUpperInvariant()).FirstOrDefault();
				if (block == null)
					throw new Exception("Invalid page block");

				return block;
            }
		}

		public T GetBlock<T>(string name) where T : BaseResource
		{
			var block = this[name];
			return (T)block.Resource;
        }

        public override abstract void Build();

		public virtual async Task OnPageLoad()
		{

		}
	}

	public class UI
	{
        public Action<PageNotificationSeverity, string, int> __Notification__ { get; set; }

        public Func<string, string, string, Task> __Alert__ { get; set; }

        public Func<string, string, string, string, Task<bool>> __Confirm__ { get; set; }

		public void Notification(PageNotificationSeverity severity, string message, int duration = 4000)
		{
			__Notification__(severity, message, duration);
		}

		public async Task Alert(string message, string title = null, string buttonText = "Ok")
		{
			await __Alert__(message, title, buttonText);
		}

        public async Task<bool> Confirm(string message, string title = null, string OkButtonText = "Ok", string CancelButtonText = "Cancel")
        {
            return await __Confirm__(message, title, OkButtonText, CancelButtonText);
        }
    }
}
