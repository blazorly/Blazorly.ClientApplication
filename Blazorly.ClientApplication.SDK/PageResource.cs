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

		public string Title { get; set; }

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
        public Func<BaseResource, string, PageDialogOption, Task<dynamic>> __Dialog__ { get; set; }
        public Func<BaseResource, string, PageSliderOption, Task<dynamic>> __Slider__ { get; set; }

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

        public async Task<T> Dialog<T>(T resource, string title, PageDialogOption options = null) where T : BaseResource
        {
            options = options != null ? options : new PageDialogOption();
            var ret =  await __Dialog__(resource, title, options);
			if (ret == true)
				return resource;

			return null;
        }

        public async Task<T> Slider<T>(T resource, string title, PageSliderOption options = null) where T : BaseResource
        {
            options = options != null ? options : new PageSliderOption();
            var ret = await __Slider__(resource, title, options);
            if (ret == true)
                return resource;

            return null;
        }
    }

	public class PageDialogOption
	{
		public string Width { get; set; } = "700px";
		public string Height { get; set; } = null;
		public bool Resizable { get; set; } = true;
        public bool Draggable { get; set; } = true;
		public string OkButtonText { get; set; } = "OK";
		public string CancelButtonText { get; set; } = "Close";
    }

	public class PageSliderOption
	{
		public string? Width { get; set; } = null;

		public PageDialogPosition Position { get; set; } = PageDialogPosition.Right;

        public string OkButtonText { get; set; } = "OK";
    }
}
