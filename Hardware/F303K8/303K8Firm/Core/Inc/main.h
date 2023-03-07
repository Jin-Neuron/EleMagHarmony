/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.h
  * @brief          : Header for main.c file.
  *                   This file contains the common defines of the application.
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2023 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __MAIN_H
#define __MAIN_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ------------------------------------------------------------------*/
#include "stm32f3xx_hal.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */

/* USER CODE END Includes */

/* Exported types ------------------------------------------------------------*/
/* USER CODE BEGIN ET */

/* USER CODE END ET */

/* Exported constants --------------------------------------------------------*/
/* USER CODE BEGIN EC */

/* USER CODE END EC */

/* Exported macro ------------------------------------------------------------*/
/* USER CODE BEGIN EM */

/* USER CODE END EM */

void HAL_TIM_MspPostInit(TIM_HandleTypeDef *htim);

/* Exported functions prototypes ---------------------------------------------*/
void Error_Handler(void);

/* USER CODE BEGIN EFP */

/* USER CODE END EFP */

/* Private defines -----------------------------------------------------------*/
#define Direction3_Pin GPIO_PIN_0
#define Direction3_GPIO_Port GPIOF
#define Step3_Pin GPIO_PIN_1
#define Step3_GPIO_Port GPIOF
#define Relay1_Pin GPIO_PIN_0
#define Relay1_GPIO_Port GPIOA
#define VCP_TX_Pin GPIO_PIN_2
#define VCP_TX_GPIO_Port GPIOA
#define Relay3_Pin GPIO_PIN_3
#define Relay3_GPIO_Port GPIOA
#define Relay2_Pin GPIO_PIN_6
#define Relay2_GPIO_Port GPIOA
#define Direction1_Pin GPIO_PIN_0
#define Direction1_GPIO_Port GPIOB
#define Step2_Pin GPIO_PIN_1
#define Step2_GPIO_Port GPIOB
#define Solenoid_Pin GPIO_PIN_8
#define Solenoid_GPIO_Port GPIOA
#define Stp_Motor_Pin GPIO_PIN_9
#define Stp_Motor_GPIO_Port GPIOA
#define Relay4_Pin GPIO_PIN_12
#define Relay4_GPIO_Port GPIOA
#define SWDIO_Pin GPIO_PIN_13
#define SWDIO_GPIO_Port GPIOA
#define SWCLK_Pin GPIO_PIN_14
#define SWCLK_GPIO_Port GPIOA
#define VCP_RX_Pin GPIO_PIN_15
#define VCP_RX_GPIO_Port GPIOA
#define Step2B5_Pin GPIO_PIN_5
#define Step2B5_GPIO_Port GPIOB
#define Direction2_Pin GPIO_PIN_6
#define Direction2_GPIO_Port GPIOB
#define Step1_Pin GPIO_PIN_7
#define Step1_GPIO_Port GPIOB

/* USER CODE BEGIN Private defines */

/* USER CODE END Private defines */

#ifdef __cplusplus
}
#endif

#endif /* __MAIN_H */
