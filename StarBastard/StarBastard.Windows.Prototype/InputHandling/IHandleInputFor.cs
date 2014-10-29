using System;

namespace StarBastard.Windows.Prototype.InputHandling
{
    public interface IHandleInputFor<in TType>
    {
        void Handle(TType target, object sender, EventArgs args);
    }
}