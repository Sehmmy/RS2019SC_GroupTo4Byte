using System;
using System.Collections.Generic;
using System.Text;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;

namespace GroupTo4Byte
{
    /// <summary>
    /// Code-behind class for the GroupTo4Byte Smart Component.
    /// </summary>
    /// <remarks>
    /// The code-behind class should be seen as a service provider used by the 
    /// Smart Component runtime. Only one instance of the code-behind class
    /// is created, regardless of how many instances there are of the associated
    /// Smart Component.
    /// Therefore, the code-behind class should not store any state information.
    /// Instead, use the SmartComponent.StateCache collection.
    /// </remarks>
    public class CodeBehind : SmartComponentCodeBehind
    {
        /// <summary>
        /// Called when the value of a dynamic property value has changed.
        /// </summary>
        /// <param name="component"> Component that owns the changed property. </param>
        /// <param name="changedProperty"> Changed property. </param>
        /// <param name="oldValue"> Previous value of the changed property. </param>
        public override void OnPropertyValueChanged(SmartComponent component, DynamicProperty changedProperty, Object oldValue)
        {
        }

        /// <summary>
        /// Called when the value of an I/O signal value has changed.
        /// </summary>
        /// <param name="component"> Component that owns the changed signal. </param>
        /// <param name="changedSignal"> Changed signal. </param>
        public override void OnIOSignalValueChanged(SmartComponent component, IOSignal changedSignal)
        {
            switch (changedSignal.Name)
            {
                case "InputGroup":
                    component.IOSignals["OutputByte3"].Value = ((int)changedSignal.Value & 0xFF000000) >> 24;
                    component.IOSignals["OutputByte2"].Value = ((int)changedSignal.Value & 0x00FF0000) >> 16;
                    component.IOSignals["OutputByte1"].Value = ((int)changedSignal.Value & 0x0000FF00) >> 8;
                    component.IOSignals["OutputByte0"].Value = ((int)changedSignal.Value & 0x000000FF);
                    break;
                case "InputByte3":
                case "InputByte2":
                case "InputByte1":
                case "InputByte0":
                    component.IOSignals["OutputGroup"].Value = (int)component.IOSignals["InputByte0"].Value + ((int)component.IOSignals["InputByte1"].Value << 8) + ((int)component.IOSignals["InputByte2"].Value << 16) + ((int)component.IOSignals["InputByte3"].Value << 24);
                    break;
                default:
                    Logger.AddMessage(new LogMessage("not recognised IO signal change"));
                    break;
            }
        }

        /// <summary>
        /// Called during simulation.
        /// </summary>
        /// <param name="component"> Simulated component. </param>
        /// <param name="simulationTime"> Time (in ms) for the current simulation step. </param>
        /// <param name="previousTime"> Time (in ms) for the previous simulation step. </param>
        /// <remarks>
        /// For this method to be called, the component must be marked with
        /// simulate="true" in the xml file.
        /// </remarks>
        public override void OnSimulationStep(SmartComponent component, double simulationTime, double previousTime)
        {
        }
    }
}
