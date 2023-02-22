/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * <h2><center>&copy; Copyright (c) 2023 STMicroelectronics.
  * All rights reserved.</center></h2>
  *
  * This software component is licensed by ST under BSD 3-Clause license,
  * the "License"; You may not use this file except in compliance with the
  * License. You may obtain a copy of the License at:
  *                        opensource.org/licenses/BSD-3-Clause
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include <math.h>
#include <stdlib.h>
#define Num 256
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
TIM_HandleTypeDef htim1;

UART_HandleTypeDef huart2;

/* USER CODE BEGIN PV */
GPIO_PinState currentState = GPIO_PIN_RESET;
uint8_t currentPosition = 0;
uint8_t isTimer1Counting = 0;

char dataLength[8];
char data[255];
char length[10];
uint8_t uartCnt = 0;
uint64_t dataLen;
uint16_t notes[10] = {0};
uint16_t freqs[10] = {0};
double buffer[10] = {0};
double tmp[10] = {0};

const double timer_clock = 84e6;
const uint16_t timer_PSC = 10 - 1;
const uint16_t timer_ARR = 160 - 1;
uint8_t arrayLen = 0;

double sampling_rate = timer_clock / (timer_PSC+1)/ (timer_ARR+1);

const double sin_offset = 2048;
const double sin_mag = 1800;

const double pi = 3.1415926535897932384626433832795;

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_USART2_UART_Init(void);
static void MX_TIM1_Init(void);
/* USER CODE BEGIN PFP */

float NoteConvert(uint16_t noteNum);
void SortArray(void);

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */
void resetStep(){
	LL_GPIO_SetOutputPin(DirectionPin_GPIO_Port, DirectionPin_Pin);
	for(uint8_t i = 0; i < 79; i++){
		LL_GPIO_SetOutputPin(StepPin_GPIO_Port, StepPin_Pin);
		LL_GPIO_ResetOutputPin(StepPin_GPIO_Port, StepPin_Pin);
	}
	currentPosition = 0;
	LL_GPIO_ResetOutputPin(DirectionPin_GPIO_Port, DirectionPin_Pin);
}
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{

    if (htim == &htim1){
		//Update position
		if(currentState == GPIO_PIN_SET){
		  if(--currentPosition <= 0){
			  currentState = GPIO_PIN_RESET;
			  LL_GPIO_ResetOutputPin(DirectionPin_GPIO_Port, DirectionPin_Pin);
		  }
		}else if(++currentPosition >= 158){
			  currentState = GPIO_PIN_SET;
			  LL_GPIO_SetOutputPin(DirectionPin_GPIO_Port, DirectionPin_Pin);
		}
		LL_GPIO_TogglePin(StepPin_GPIO_Port, StepPin_Pin);
    }
}
void buzzer_pwmout(TIM_HandleTypeDef htim, uint32_t prescaler, uint32_t pwm){

  if(pwm > 99) pwm = 99;
  if(prescaler < 0) prescaler = 799;

  htim.Instance = htim.Instance;
  htim.Init.Prescaler = prescaler;
  htim.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim.Init.Period = 99;
  htim.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  HAL_TIM_Base_Init(&htim);

  // start
  HAL_TIM_Base_Start_IT(&htim);

}
/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_USART2_UART_Init();
  MX_TIM1_Init();
  /* USER CODE BEGIN 2 */
  resetStep();
  HAL_UART_Receive_IT(&huart2, (uint8_t *)dataLength, 8);
  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Configure the main internal regulator output voltage
  */
  __HAL_RCC_PWR_CLK_ENABLE();
  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE2);
  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI;
  RCC_OscInitStruct.PLL.PLLM = 16;
  RCC_OscInitStruct.PLL.PLLN = 336;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV4;
  RCC_OscInitStruct.PLL.PLLQ = 7;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 799;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 99;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_ENABLE;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */

}

/**
  * @brief USART2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART2_UART_Init(void)
{

  /* USER CODE BEGIN USART2_Init 0 */

  /* USER CODE END USART2_Init 0 */

  /* USER CODE BEGIN USART2_Init 1 */

  /* USER CODE END USART2_Init 1 */
  huart2.Instance = USART2;
  huart2.Init.BaudRate = 115200;
  huart2.Init.WordLength = UART_WORDLENGTH_8B;
  huart2.Init.StopBits = UART_STOPBITS_1;
  huart2.Init.Parity = UART_PARITY_NONE;
  huart2.Init.Mode = UART_MODE_TX_RX;
  huart2.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart2.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART2_Init 2 */

  /* USER CODE END USART2_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  LL_EXTI_InitTypeDef EXTI_InitStruct = {0};
  LL_GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  LL_AHB1_GRP1_EnableClock(LL_AHB1_GRP1_PERIPH_GPIOC);
  LL_AHB1_GRP1_EnableClock(LL_AHB1_GRP1_PERIPH_GPIOH);
  LL_AHB1_GRP1_EnableClock(LL_AHB1_GRP1_PERIPH_GPIOA);
  LL_AHB1_GRP1_EnableClock(LL_AHB1_GRP1_PERIPH_GPIOB);

  /**/
  LL_GPIO_ResetOutputPin(GPIOA, LD2_Pin|StepPin_Pin|DirectionPin_Pin);

  /**/
  LL_SYSCFG_SetEXTISource(LL_SYSCFG_EXTI_PORTC, LL_SYSCFG_EXTI_LINE13);

  /**/
  EXTI_InitStruct.Line_0_31 = LL_EXTI_LINE_13;
  EXTI_InitStruct.LineCommand = ENABLE;
  EXTI_InitStruct.Mode = LL_EXTI_MODE_IT;
  EXTI_InitStruct.Trigger = LL_EXTI_TRIGGER_FALLING;
  LL_EXTI_Init(&EXTI_InitStruct);

  /**/
  LL_GPIO_SetPinPull(B1_GPIO_Port, B1_Pin, LL_GPIO_PULL_NO);

  /**/
  LL_GPIO_SetPinMode(B1_GPIO_Port, B1_Pin, LL_GPIO_MODE_INPUT);

  /**/
  GPIO_InitStruct.Pin = LD2_Pin|StepPin_Pin|DirectionPin_Pin;
  GPIO_InitStruct.Mode = LL_GPIO_MODE_OUTPUT;
  GPIO_InitStruct.Speed = LL_GPIO_SPEED_FREQ_LOW;
  GPIO_InitStruct.OutputType = LL_GPIO_OUTPUT_PUSHPULL;
  GPIO_InitStruct.Pull = LL_GPIO_PULL_NO;
  LL_GPIO_Init(GPIOA, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
	if(uartCnt){
		uint8_t part = 0;
		uint64_t lastIdx = 0;
		for(uint64_t i = 0; i < dataLen; i++){
			uint8_t Idx = i - lastIdx;
			data[i] &= 0x0f;
			if(Idx < 4){
				part |= data[i] << (3 - Idx);
			}else if(Idx < 5){
				if(data[i] == 0){
					lastIdx = i + 1;
					freqs[part] = 0;
					notes[part] = 0;
					buffer[part] = 0;
					part = 0;
				}
			}else if(Idx < 13){
				notes[part] |= data[i] << (12 - Idx);
			}else{
				freqs[part] = (uint16_t)NoteConvert(notes[part]);
				buffer[part] = (double)freqs[part] * Num / sampling_rate;
				lastIdx = i;
				part = 0;
			}
			if(i == dataLen - 1){
				freqs[part] = (uint16_t)NoteConvert(notes[part]);
				buffer[part] = (double)freqs[part] * Num / sampling_rate;
				if(Idx < 5){
					lastIdx = i + 1;
					freqs[part] = 0;
					notes[part] = 0;
					buffer[part] = 0;
					part = 0;
				}
			}
		}
		SortArray();
		for(uint8_t i = 0; i < 5; i++){
		}
		if(notes[1] != 0){
			uint16_t duty = 65;
			/*if(notes[i] < 30){
				duty = 20;
			}else if(notes[i] < 40){
				duty = 30;
			}else if(notes[i] < 50){
				duty = 35;
			}else if(notes[i] < 60){
				duty = 40;
			}else if(notes[i] < 70){
				duty = 45;
			}else if(notes[i] < 75){
				duty = 50;
			}else if(notes[i] < 80){
				duty = 55;
			}else{
				duty = 60;
			}*/
			buzzer_pwmout(htim1, (timer_clock / (freqs[1] * 200) - 1), 65);
		}else{
			HAL_TIM_Base_Stop_IT(&htim1);
		}
		uartCnt = 0;
		HAL_UART_Receive_IT(&huart2, (uint8_t *)dataLength, 8);
		return;
	}
	dataLen = 0;
	for(uint8_t i = 0; i < 8; i++){
		dataLength[i] &= 0x0f;
		dataLen |= dataLength[i] << (7 - i);
	}
	HAL_UART_Receive_IT(&huart2, (uint8_t *)data, dataLen);
	uartCnt++;
}

float NoteConvert(uint16_t noteNum){
	int64_t tmp = noteNum - 69;
	double exp = (double)tmp / 12.0;
	double freq = 440 * pow(2.0, exp);
	return (float)freq;
}
void SortArray(){
	for(uint8_t i = 0; i < 10; i++){
		if(buffer[i] == 0){
			arrayLen = (i == 0) ? 1 : i;
			uint8_t l = 1;
			for(uint8_t j = i; j < 10; j++){
				if(j > arrayLen * (l + 1))	l++;
				freqs[j] = freqs[j-arrayLen*l];
				notes[j] = notes[j-arrayLen*l];
			}
			return;
		}
	}
}

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
