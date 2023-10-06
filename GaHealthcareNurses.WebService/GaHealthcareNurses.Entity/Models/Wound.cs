using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class Wound
    {
        public int WoundId { get; set; }
        public int? AdultAssessmentId { get; set; }
        public int WoundNumber { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Stage { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Square { get; set; }
        public double Depth { get; set; }
        public string SorroundingTissue { get; set; }
        public string BedTissue { get; set; }
        public string Undermining { get; set; }
        public string Tunneling { get; set; }
        public bool STIsNormal { get; set; }
        public bool STIsPale { get; set; }
        public bool STIsRed { get; set; }
        public bool STIsEdema { get; set; }
        public bool STIsBlanched { get; set; }
        public bool STIsPurple { get; set; }
        public bool STIsCool { get; set; }
        public bool STIsShiny { get; set; }
        public bool STIsBlack { get; set; }
        public bool STIsWarm { get; set; }
        public bool IsSTIsOther { get; set; }
        public string STIsOther { get; set; }
        public string WoundBedColor { get; set; }
        public bool WBTIsBloody { get; set; }
        public bool WBTIsGranular { get; set; }
        public bool WBTIsSloughing { get; set; }
        public bool WBTIsWeeping { get; set; }
        public bool WBTIsNecrotic { get; set; }
        public bool WBTIsHealthy { get; set; }
        public bool WBTIsEschar { get; set; }
        public bool WBTIsOther { get; set; }
        public string WBTOther { get; set; }
        public string DrainageType { get; set; }
        public string DrainageAmount { get; set; }
        public string DrainageOdor { get; set; }
        public bool IsNotDueToday { get; set; }
        public string CleansedWith { get; set; }
        public string PackedWith { get; set; }
        public string DressedWith { get; set; }
        public string CoveredWith { get; set; }
        public string SecuredWith { get; set; }
        public bool NPWTIsFoam { get; set; }
        public bool NPWTIsBlack { get; set; }
        public bool NPWTIsWhite { get; set; }
        public bool NPWTIsGreen { get; set; }
        public string NPWTPiece { get; set; }
        public string NPWTPeriwound { get; set; }
        public string NPWTWoundBed { get; set; }
        public string NPWTPressure { get; set; }
        public string NPWTContOrIntermittent { get; set; }
        public bool NPWTIsCanisterChange { get; set; }
        public string PatientResponse { get; set; }
        public bool IsSeeWoundAddendum { get; set; }
        public bool IsMattressPressureRefiefDevice { get; set; }
        public bool IsOverlayPressureRefiefDevice { get; set; }
        public bool IsWCCushionPressureRefiefDevice { get; set; }
        public bool IsOtherPressureRefiefDevice { get; set; }
        public string OtherPressureRefiefDevice { get; set; }
        public string AdditionalComments { get; set; }
        public virtual AdultAssessment AdultAssessment { get; set; }
    }
}
