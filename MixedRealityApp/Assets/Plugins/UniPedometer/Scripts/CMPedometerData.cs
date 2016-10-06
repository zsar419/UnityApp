using UnityEngine;
using System.Collections;
using System;

namespace UniPedometer
{
	
	[System.Serializable]
	public class CMPedometerData {
		[SerializeField] int startDate;
		[SerializeField] int endDate;
		[SerializeField] int numberOfSteps;
		[SerializeField] int distance;
		[SerializeField] int? currentPace;
		[SerializeField] int? currentCadence;
		[SerializeField] int? floorsAscended;
		[SerializeField] int? floorsDescended;

		public static DateTime BaseDateTime {
			get {
				return new DateTime (1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			}
		}

		public DateTime StartDate {
			get {
				return BaseDateTime.AddSeconds (startDate).ToLocalTime();
			}
		}

		public DateTime EndDate {
			get {
				return BaseDateTime.AddSeconds (endDate).ToLocalTime();
			}
		}

		public int NumberOfSteps {
			get {
				return numberOfSteps;
			}
		}

		public int Distance {
			get {
				return distance;
			}
		}

		public bool HasCurrentPase {
			get {
				return currentPace.HasValue;
			}
		}

		public int CurrentPace {
			get {
				return currentPace.Value;
			}
		}

		public bool HasCurrentCadence {
			get {
				return currentCadence.HasValue;
			}
		}

		public int CurrentCadence {
			get {
				return currentCadence.Value;
			}
		}

		public bool HasFloorsAscended {
			get {
				return floorsAscended.HasValue;
			}
		}

		public int FloorsAscended {
			get {
				return floorsAscended.Value;
			}
		}

		public bool HasFloorsDescended {
			get {
				return floorsDescended.HasValue;
			}
		}

		public int FloorsDescended {
			get {
				return floorsDescended.Value;
			}
		}
	}

}